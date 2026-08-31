using Clothes_Shop_ERP.DAL;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaymentMethodEntity = Clothes_Shop_ERP.DAL.PaymentMethods;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcPaymentMethods : DevExpress.XtraEditors.XtraUserControl
    {
        public UcPaymentMethods()
        {
            InitializeComponent();
            GetData();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.PaymentMethods
                    .Select(x => new { x.Id, x.Name })
                    .ToList();
            }
        }
        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var hit = gridView1.CalcHitInfo(e.Location);
            if (hit.InRow) gridView1.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            menu.Items.Add("New", null, (s, ev) => AddNew());

            if (hit.InRow)
            {
                menu.Items.Add("Edit", null, (s, ev) => EditSelected());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }

            menu.Show(gridControl1, e.Location);
        }
        private void AddNew()
        {
            string name = XtraInputBox.Show("Payment method name:", "New Payment Method", "");
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var db = new ClothesShopDBContext())
            {
                db.PaymentMethods.Add(new PaymentMethodEntity { Name = name });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Payment method added");
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();

            string newName = XtraInputBox.Show("Enter new name:", $"Editing: {currentName}", currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            using (var db = new ClothesShopDBContext())
            {
                var method = db.PaymentMethods.Where(x => x.Id == id).FirstOrDefault();
                if (method == null) { Sett.MsgBlue("Error", $"No item found with Id = {id}"); return; }
                method.Name = newName;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Payment method updated");
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();

            if (XtraMessageBox.Show($"Delete '{name}'?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var method = db.PaymentMethods.Where(x => x.Id == id).FirstOrDefault();
                    if (method != null) db.PaymentMethods.Remove(method);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Payment method deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This payment method is used by existing invoices. It can't be removed.");
            }
        }
    }
}
