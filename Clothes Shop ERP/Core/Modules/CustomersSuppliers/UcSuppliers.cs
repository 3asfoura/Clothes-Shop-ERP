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
            Sett.EnableMultiSelect(gridView1);
            Sett.CenterColumns(gridView1);
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

        // One confirmation for the whole selection (Ctrl/Shift+click to pick several),
        // and a row the database refuses because it is still referenced elsewhere is
        // reported at the end instead of stopping the rest of the batch.
        private void DeleteSelected()
        {
            Sett.DeleteSelectedRows(gridView1, GetData, id =>
            {
                using (var db = new ClothesShopDBContext())
                {
                    var row = db.Suppliers.FirstOrDefault(x => x.Id == id);
                    if (row != null) db.Suppliers.Remove(row);
                    db.SaveChanges();
                }
            });
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
            bool canEdit = PermissionManager.CanEdit("Suppliers");
            if (canEdit) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow && canEdit)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected());
                if (PermissionManager.CanDelete("Suppliers"))
                    menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected());
            }
            menu.Items.Add(LocalizationManager.T("Shared_MenuExport"), null, (s, ev) => Sett.ExportGrid(gridControl1, LocalizationManager.T("Main_Suppliers")));
        }
    }
}
