using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
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
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColName.Caption = LocalizationManager.T("Shared_Name");
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
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }

            menu.Show(gridControl1, e.Location);
        }
        private void AddNew()
        {
            string name = XtraInputBox.Show(LocalizationManager.T("PaymentMethods_NamePrompt"), LocalizationManager.T("PaymentMethods_NewTitle"), "");
            if (string.IsNullOrWhiteSpace(name)) return;

            using (var db = new ClothesShopDBContext())
            {
                db.PaymentMethods.Add(new PaymentMethodEntity { Name = name });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("PaymentMethods_EntityName")));
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();

            string newName = XtraInputBox.Show(LocalizationManager.T("PaymentMethods_EditNamePrompt"), string.Format(LocalizationManager.T("PaymentMethods_EditingTitleFmt"), currentName), currentName);
            if (string.IsNullOrWhiteSpace(newName)) return;

            using (var db = new ClothesShopDBContext())
            {
                var method = db.PaymentMethods.Where(x => x.Id == id).FirstOrDefault();
                if (method == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("PaymentMethods_EntityName"), id)); return; }
                method.Name = newName;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("PaymentMethods_EntityName")));
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();

            if (XtraMessageBox.Show(string.Format(LocalizationManager.T("Common_ConfirmDelete"), name), LocalizationManager.T("Common_ConfirmTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var method = db.PaymentMethods.Where(x => x.Id == id).FirstOrDefault();
                    if (method != null) db.PaymentMethods.Remove(method);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("PaymentMethods_EntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("PaymentMethods_InUse"));
            }
        }
    }
}
