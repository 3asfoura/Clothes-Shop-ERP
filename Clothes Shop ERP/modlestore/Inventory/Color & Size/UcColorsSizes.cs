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
using ColorEntity = Clothes_Shop_ERP.DAL.Colors;
using SizeEntity = Clothes_Shop_ERP.DAL.Sizes;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcColorsSizes : DevExpress.XtraEditors.XtraUserControl
    {
        public UcColorsSizes()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView2.OptionsView.ShowGroupPanel = false;
            gridView2.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView2.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            groupControl1.Text = LocalizationManager.T("ColorsSizes_Colors");
            groupControl3.Text = LocalizationManager.T("ColorsSizes_Sizes");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.Colors
                    .Select(x => new { x.Id, x.Name, x.HexCode })
                    .ToList();
            }
            if (gridView1.Columns["Name"] != null) gridView1.Columns["Name"].Caption = LocalizationManager.T("Shared_Name");
            if (gridView1.Columns["HexCode"] != null) gridView1.Columns["HexCode"].Caption = LocalizationManager.T("ColorsSizes_ColHexCode");

            using (var db = new ClothesShopDBContext())
            {
                gridView2.GridControl.DataSource = db.Sizes
                    .OrderBy(x => x.SortOrder)
                    .Select(x => new { x.Id, x.Name, x.SortOrder })
                    .ToList();
            }
            if (gridView2.Columns["Name"] != null) gridView2.Columns["Name"].Caption = LocalizationManager.T("Shared_Name");
            if (gridView2.Columns["SortOrder"] != null) gridView2.Columns["SortOrder"].Caption = LocalizationManager.T("ColorsSizes_ColSortOrder");
        }
        private void UcColorsSizes_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }
        private void AddNew_Color()
        {
            var form = new FrmColorEdit(LocalizationManager.T("ColorsSizes_NewColorTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Colors.Add(new ColorEntity { Name = form.ColorName, HexCode = form.HexCode });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("ColorsSizes_ColorEntityName")));
            GetData();
        }

        private void EditSelected_Color()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            string currentHex = gridView1.GetFocusedRowCellValue("HexCode")?.ToString() ?? "";

            var form = new FrmColorEdit(string.Format(LocalizationManager.T("ColorsSizes_EditingColorTitleFmt"), currentName), currentName, currentHex);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var color = db.Colors.Where(x => x.Id == id).FirstOrDefault();
                if (color == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("ColorsSizes_ColorEntityName"), id)); return; }
                color.Name = form.ColorName;
                color.HexCode = form.HexCode;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("ColorsSizes_ColorEntityName")));
            GetData();
        }

        private void DeleteSelected_Color()
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
                    var color = db.Colors.Where(x => x.Id == id).FirstOrDefault();
                    if (color == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("ColorsSizes_ColorEntityName"), id)); return; }
                    db.Colors.Remove(color);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("ColorsSizes_ColorEntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("ColorsSizes_ColorInUse"));
            }
        }

        private void AddNew_Size()
        {
            var form = new FrmSizeEdit(LocalizationManager.T("ColorsSizes_NewSizeTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Sizes.Add(new SizeEntity { Name = form.SizeName, SortOrder = form.SortOrder });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XAdded"), LocalizationManager.T("ColorsSizes_SizeEntityName")));
            GetData();
        }

        private void EditSelected_Size()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            int currentSort = Convert.ToInt32(gridView1.GetFocusedRowCellValue("SortOrder"));

            var form = new FrmSizeEdit(string.Format(LocalizationManager.T("ColorsSizes_EditingSizeTitleFmt"), currentName), currentName, currentSort);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var size = db.Sizes.Where(x => x.Id == id).FirstOrDefault();
                if (size == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("ColorsSizes_SizeEntityName"), id)); return; }
                size.Name = form.SizeName;
                size.SortOrder = form.SortOrder;
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XUpdated"), LocalizationManager.T("ColorsSizes_SizeEntityName")));
            GetData();
        }

        private void DeleteSelected_Size()
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
                    var size = db.Sizes.Where(x => x.Id == id).FirstOrDefault();
                    if (size == null) { Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Shared_NoXFoundWithId"), LocalizationManager.T("ColorsSizes_SizeEntityName"), id)); return; }
                    db.Sizes.Remove(size);
                    db.SaveChanges();
                }
                Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Shared_XDeleted"), LocalizationManager.T("ColorsSizes_SizeEntityName")));
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_CannotDelete"), LocalizationManager.T("ColorsSizes_SizeInUse"));
            }
        }
        private void gridView2_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "HexCode") return;

            string hex = e.CellValue?.ToString();
            if (string.IsNullOrWhiteSpace(hex)) return;

            try
            {
                Color bg = ColorTranslator.FromHtml(hex);
                e.Appearance.BackColor = bg;
                e.Appearance.ForeColor = GetContrastColor(bg);
            }
            catch
            {

            }
        }
        private Color GetContrastColor(Color bg)
        {

            double brightness = (bg.R * 299 + bg.G * 587 + bg.B * 114) / 1000.0;
            return brightness > 125 ? Color.Black : Color.White;
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = gridView2.CalcHitInfo(e.Location);
            if (hit.InRow)
                gridView2.FocusedRowHandle = hit.RowHandle;
            if (hit.InColumnPanel || hit.InColumn)
                return;
            var menu = new ContextMenuStrip();
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew_Size());
            menu.Show(gridControl2, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected_Size());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected_Size());
            }
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
            menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew_Color());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuEdit"), null, (s, ev) => EditSelected_Color());
                menu.Items.Add(LocalizationManager.T("Shared_MenuDelete"), null, (s, ev) => DeleteSelected_Color());
            }
        }
    }
}
