using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Resources;
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
using BranchEntity = Clothes_Shop_ERP.DAL.Branches;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcBranches : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBranches()
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
                gridControl1.DataSource = db.Branches
                    //     .Select(x => new { x.Id , x.Name })
                    .ToList();
            }
        }
       
        private void UcBranches_Load(object sender, EventArgs e)
        {
            GetData();
        }
        private void AddNew()
        {
            var form = new FrmBranchEdit("New Branch");

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var db = new ClothesShopDBContext())
                {
                    db.Branches.Add(new BranchEntity
                    {
                        Name = form.BranchName,
                        Address = form.BranchAddress,
                        Phone = form.BranchPhone,
                        IsActive = true
                    });
                    db.SaveChanges();
                }

                Sett.MsgGreen("Success", "Branch added");
                GetData();
            }
        }
        private void ToggleActive()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();
            bool currentStatus = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsActive"));

            string action = currentStatus ? "Deactivate" : "Activate";

            if (XtraMessageBox.Show($"{action} '{name}'?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var branch = db.Branches.Where(r => r.Id == id).FirstOrDefault();
                if (branch == null) return;

                branch.IsActive = !currentStatus;
                db.SaveChanges();
            }

            Sett.MsgBlue("Success", $"Branch {action.ToLower()}d");
            GetData();
        }
        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string currentName = gridView1.GetFocusedRowCellValue("Name").ToString();
            string currentAddress = gridView1.GetFocusedRowCellValue("Address")?.ToString() ?? "";
            string currentPhone = gridView1.GetFocusedRowCellValue("Phone")?.ToString() ?? "";

            var form = new FrmBranchEdit($"Editing Branch: {currentName}", currentName, currentAddress, currentPhone);

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var db = new ClothesShopDBContext())
                {
                    var branch = db.Branches.Where(r => r.Id == id).FirstOrDefault();

                    if (branch == null)
                    {
                        Sett.MsgBlue("Error", $"No branch found with Id = {id}");
                        return;
                    }

                    branch.Name = form.BranchName;
                    branch.Address = form.BranchAddress;
                    branch.Phone = form.BranchPhone;
                    db.SaveChanges();
                }

                Sett.MsgBlue("Success", "Branch updated");
                GetData();
            }
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;

            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));
            string name = gridView1.GetFocusedRowCellValue("Name").ToString();

            if (XtraMessageBox.Show($"Delete '{name}'?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using (var db = new ClothesShopDBContext())
                {
                    var branch = db.Branches.Where(r => r.Id == id).FirstOrDefault();

                    if (branch == null)
                    {
                        Sett.MsgBlue("Error", $"No branch found with Id = {id}");
                        return;
                    }

                    db.Branches.Remove(branch);
                    db.SaveChanges();
                }

                Sett.MsgBlue("Success", "Branch deleted");
                GetData();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Sett.MsgBlue("Cannot Delete", "This branch has related data (users, invoices, stock...). Remove those first.");
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            
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
                menu.Items.Add("Activate/Deactivate", null, (s, ev) => ToggleActive());
                menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }
        }
    }
}
