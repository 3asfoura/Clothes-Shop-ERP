using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PurchaseReturnEntity = Clothes_Shop_ERP.DAL.PurchaseReturns;
using PurchaseReturnDetailEntity = Clothes_Shop_ERP.DAL.PurchaseReturnDetails;
using StockMovementEntity = Clothes_Shop_ERP.DAL.StockMovements;
using TreasuryEntity = Clothes_Shop_ERP.DAL.TreasuryTransactions;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcPurchaseReturns : DevExpress.XtraEditors.XtraUserControl
    {
        public UcPurchaseReturns()
        {
            InitializeComponent();
            GetData();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColSupplier.Caption = LocalizationManager.T("Purchases_ColSupplier");
            ColBranch.Caption = LocalizationManager.T("Shared_Branch");
            ColReturnDate.Caption = LocalizationManager.T("Returns_ColReturnDate");
            ColTotalAmount.Caption = LocalizationManager.T("Shared_TotalAmount");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.PurchaseReturns
                    .Include(x => x.PurchaseInvoice).ThenInclude(i => i.Supplier)
                    .Include(x => x.Branch)
                    .OrderByDescending(x => x.ReturnDate)
                    .Select(x => new
                    {
                        x.Id,
                        Supplier = x.PurchaseInvoice.Supplier.Name,
                        Branch = x.Branch.Name,
                        x.ReturnDate,
                        x.TotalAmount
                    })
                    .ToList();
            }
        }

        private void AddNew()
        {
            var form = new FrmPurchaseReturnEdit(LocalizationManager.T("PurchaseReturns_NewTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            int branchId = FrmLogin.CurrentBranchId;
            decimal total = form.UnitCost * form.Quantity;

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var purchaseReturn = new PurchaseReturnEntity
                    {
                        PurchaseInvoiceId = form.PurchaseInvoiceId,
                        BranchId = branchId,
                        ReturnDate = DateTime.Now,
                        TotalAmount = total,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    };
                    db.PurchaseReturns.Add(purchaseReturn);
                    db.SaveChanges();   // generates purchaseReturn.Id for the detail row below

                    db.PurchaseReturnDetails.Add(new PurchaseReturnDetailEntity
                    {
                        PurchaseReturnId = purchaseReturn.Id,
                        ProductVariantId = form.ProductVariantId,
                        Quantity = form.Quantity,
                        UnitCost = form.UnitCost,
                        Total = total
                    });

                    // Take the stock back out - it's going back to the supplier
                    int rowsAffected = db.Database.ExecuteSqlCommand(
                        "UPDATE BranchStock SET Quantity = Quantity - {0} WHERE ProductVariantId = {1} AND BranchId = {2} AND Quantity >= {0}",
                        form.Quantity, form.ProductVariantId, branchId);

                    if (rowsAffected == 0)
                    {
                        transaction.Rollback();
                        Sett.MsgBlue(LocalizationManager.T("POS_OutOfStockTitle"), LocalizationManager.T("PurchaseReturns_NotEnoughStock"));
                        return;
                    }

                    db.StockMovements.Add(new StockMovementEntity
                    {
                        ProductVariantId = form.ProductVariantId,
                        BranchId = branchId,
                        MovementType = "PurchaseReturn",
                        Quantity = -form.Quantity,
                        RefType = "PurchaseReturn",
                        RefId = purchaseReturn.Id,
                        CreatedAt = DateTime.Now,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    });

                    // Money comes back from the supplier
                    db.TreasuryTransactions.Add(new TreasuryEntity
                    {
                        BranchId = branchId,
                        TransactionType = "In",
                        Amount = total,
                        Description = "Purchase return",
                        RefType = "PurchaseReturn",
                        RefId = purchaseReturn.Id,
                        CreatedAt = DateTime.Now,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    });

                    db.SaveChanges();
                    transaction.Commit();

                    Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("PurchaseReturns_Recorded"), total));
                    GetData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("PurchaseReturns_SaveFailed"), ex.Message));
                }
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
            if (PermissionManager.CanEdit("PurchaseReturns")) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);
        }
    }
}
