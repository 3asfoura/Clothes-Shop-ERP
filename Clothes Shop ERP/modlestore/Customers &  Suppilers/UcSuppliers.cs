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
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColName.Caption = LocalizationManager.T("Shared_Name");
            ColPhone.Caption = LocalizationManager.T("Shared_Phone");
            ColAddress.Caption = LocalizationManager.T("Shared_Address");
            ColIsActive.Caption = LocalizationManager.T("Shared_IsActive");
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
            var form = new FrmPartyEdit(LocalizationManager.T("Suppliers_NewTitle"));
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
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("Suppliers_EntityName")));
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

            var form = new FrmPartyEdit(string.Format(LocalizationManager.T("Party_EditingTitleFmt"), currentName), currentName, currentAddress, currentPhone, currentActive);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var supplier = db.Suppliers.Where(x => x.Id == id).FirstOrDefault();
                if (supplier == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("Suppliers_EntityName"), id)); return; }
                supplier.Name = form.PartyName;
                supplier.Address = form.Address;
                supplier.Phone = form.Phone;
                supplier.IsActive = form.IsActive;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("Suppliers_EntityName")));
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
                    var supplier = db.Suppliers.Where(x => x.Id == id).FirstOrDefault();
                    if (supplier != null) db.Suppliers.Remove(supplier);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("Suppliers_EntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("Suppliers_HasInvoices"));
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
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }
        }
    }
}
