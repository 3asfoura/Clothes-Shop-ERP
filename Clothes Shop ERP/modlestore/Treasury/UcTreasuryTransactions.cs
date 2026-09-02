using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TreasuryEntity = Clothes_Shop_ERP.DAL.TreasuryTransactions;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcTreasuryTransactions : DevExpress.XtraEditors.XtraUserControl
    {
        public UcTreasuryTransactions()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColBranch.Caption = LocalizationManager.T("Treasury_ColBranch");
            ColTransactionType.Caption = LocalizationManager.T("Treasury_ColTransactionType");
            ColAmount.Caption = LocalizationManager.T("Treasury_ColAmount");
            ColDescription.Caption = LocalizationManager.T("Treasury_ColDescription");
            ColCreatedAt.Caption = LocalizationManager.T("Treasury_ColCreatedAt");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.TreasuryTransactions
                    .Include(x => x.Branch)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new
                    {
                        x.Id,
                        Branch = x.Branch.Name,
                        x.TransactionType,
                        x.Amount,
                        x.Description,
                        x.CreatedAt
                    })
                    .ToList();
            }
        }
        private void UcTreasuryTransactions_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            //if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Row) return;
            //if (e.HitInfo.InRow) gridView1.FocusedRowHandle = e.HitInfo.RowHandle;

            //e.Menu.Items.Clear();
            //e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("New", (s, ev) => AddNew()));
            //if (e.HitInfo.InRow)
            //{
            //    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Edit", (s, ev) => EditSelected()));
            //    e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Delete", (s, ev) => DeleteSelected()));
            //}
        }
        private void AddNew()
        {
            var form = new FrmTreasuryEdit("New Treasury Entry");
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                db.TreasuryTransactions.Add(new TreasuryEntity
                {
                    BranchId = form.BranchId,
                    TransactionType = form.TransactionType,
                    Amount = form.Amount,
                    Description = form.Description,
                    RefType = "Manual",
                    RefId = null,
                    CreatedByUserId = FrmLogin.CurrentUserId
                });
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Treasury entry added");
            GetData();
        }

        private void EditSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            TreasuryTransactions current;
            using (var db = new ClothesShopDBContext())
                current = db.TreasuryTransactions.Where(x => x.Id == id).FirstOrDefault();

            if (current == null) { Sett.MsgBlue("Error", $"No entry found with Id = {id}"); return; }

            var form = new FrmTreasuryEdit("Editing Entry", current.TransactionType, current.Amount,
                current.Description, current.BranchId);

            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            {
                var entry = db.TreasuryTransactions.Where(x => x.Id == id).FirstOrDefault();
                entry.BranchId = form.BranchId;
                entry.TransactionType = form.TransactionType;
                entry.Amount = form.Amount;
                entry.Description = form.Description;
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Treasury entry updated");
            GetData();
        }

        private void DeleteSelected()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            if (XtraMessageBox.Show("Delete this entry?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new ClothesShopDBContext())
            {
                var entry = db.TreasuryTransactions.Where(x => x.Id == id).FirstOrDefault();
                if (entry != null) db.TreasuryTransactions.Remove(entry);
                db.SaveChanges();
            }
            Sett.MsgBlue("Success", "Treasury entry deleted");
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
