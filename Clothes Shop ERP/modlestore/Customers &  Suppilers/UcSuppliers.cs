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
using SupplierEntity = Clothes_Shop_ERP.DAL.Suppliers;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcSuppliers : DevExpress.XtraEditors.XtraUserControl
    {
        public UcSuppliers()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.Suppliers
                    .Select(x => new { x.Id, x.Name, x.Phone, x.Address, x.IsActive })
                    .ToList();
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            
        }
        private void AddNew()
        {
            var form = new FrmPartyEdit("New Supplier");
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Suppliers.Add(new SupplierEntity
                {
                    Name = form.PartyName,
                    Address = form.Address,
                    Phone = form.Phone,
                    IsActive = form.IsActive
                });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Supplier added");
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            string currentAddress = gridView1.GetFocusedRowCellValue("Address")?.ToString() ?? "";
            string currentPhone = gridView1.GetFocusedRowCellValue("Phone")?.ToString() ?? "";
            bool currentActive = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsActive"));

            var form = new FrmPartyEdit($"Editing: {currentName}", currentName, currentAddress, currentPhone, currentActive);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var supplier = db.Suppliers.Where(x => x.Id == id).FirstOrDefault();
                if (supplier == null) { Sett.MsgBlue("Error", $"No supplier found with Id = {id}"); return; }
                supplier.Name = form.PartyName;
                supplier.Address = form.Address;
                supplier.Phone = form.Phone;
                supplier.IsActive = form.IsActive;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Supplier updated");
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
                    var supplier = db.Suppliers.Where(x => x.Id == id).FirstOrDefault();
                    if (supplier != null) db.Suppliers.Remove(supplier);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Supplier deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This supplier has purchase invoices linked to it. Deactivate instead.");
            }
        }

        private void UcSuppliers_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = gridView1.CalcHitInfo(e.Location);
            if (hit.InRow)
                gridView1.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            menu.Items.Add("New", null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add("Edit", null, (s, ev) => EditSelected());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }
        }
    }
}
