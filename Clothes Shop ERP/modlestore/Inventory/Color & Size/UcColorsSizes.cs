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
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.Colors
                    .Select(x => new { x.Id, x.Name, x.HexCode })
                    .ToList();
            }
            using (var db = new ClothesShopDBContext())
            {
                gridView2.GridControl.DataSource = db.Sizes
                    .OrderBy(x => x.SortOrder)
                    .Select(x => new { x.Id, x.Name, x.SortOrder })
                    .ToList();
            }
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
            var form = new FrmColorEdit("New Color");
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Colors.Add(new ColorEntity { Name = form.ColorName, HexCode = form.HexCode });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Color added");
            GetData();
        }

        private void EditSelected_Color()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            string currentHex = gridView1.GetFocusedRowCellValue("HexCode")?.ToString() ?? "";

            var form = new FrmColorEdit($"Editing Color: {currentName}", currentName, currentHex);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var color = db.Colors.Where(x => x.Id == id).FirstOrDefault();
                if (color == null) { Sett.MsgBlue("Error", $"No color found with Id = {id}"); return; }
                color.Name = form.ColorName;
                color.HexCode = form.HexCode;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Color updated");
            GetData();
        }

        private void DeleteSelected_Color()
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
                    var color = db.Colors.Where(x => x.Id == id).FirstOrDefault();
                    if (color == null) { Sett.MsgBlue("Error", $"No color found with Id = {id}"); return; }
                    db.Colors.Remove(color);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Color deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This color is used by one or more product variants. Remove those first.");
            }
        }

        private void AddNew_Size()
        {
            var form = new FrmSizeEdit("New Size");
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.Sizes.Add(new SizeEntity { Name = form.SizeName, SortOrder = form.SortOrder });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Size added");
            GetData();
        }

        private void EditSelected_Size()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            int currentSort = Convert.ToInt32(gridView1.GetFocusedRowCellValue("SortOrder"));

            var form = new FrmSizeEdit($"Editing Size: {currentName}", currentName, currentSort);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var size = db.Sizes.Where(x => x.Id == id).FirstOrDefault();
                if (size == null) { Sett.MsgBlue("Error", $"No size found with Id = {id}"); return; }
                size.Name = form.SizeName;
                size.SortOrder = form.SortOrder;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Size updated");
            GetData();
        }

        private void DeleteSelected_Size()
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
                    var size = db.Sizes.Where(x => x.Id == id).FirstOrDefault();
                    if (size == null) { Sett.MsgBlue("Error", $"No size found with Id = {id}"); return; }
                    db.Sizes.Remove(size);
                    db.SaveChanges();
                }
                Sett.MsgBlue("Success", "Size deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This size is used by one or more product variants. Remove those first.");
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
            menu.Items.Add("New", null, (s, ev) => AddNew_Size());
            menu.Show(gridControl2, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add("Edit", null, (s, ev) => EditSelected_Size());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected_Size());
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
            menu.Items.Add("New", null, (s, ev) => AddNew_Color());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add("Edit", null, (s, ev) => EditSelected_Color());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected_Color());
            }
        }
    }
}
