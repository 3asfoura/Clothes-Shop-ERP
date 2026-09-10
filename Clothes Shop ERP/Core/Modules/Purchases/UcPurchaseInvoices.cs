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
using BranchStockEntity = Clothes_Shop_ERP.DAL.BranchStock;
using PurchaseInvoiceDetailEntity = Clothes_Shop_ERP.DAL.PurchaseInvoiceDetails;
using PurchaseInvoiceEntity = Clothes_Shop_ERP.DAL.PurchaseInvoices;
using StockMovementEntity = Clothes_Shop_ERP.DAL.StockMovements;
using TreasuryEntity = Clothes_Shop_ERP.DAL.TreasuryTransactions;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcPurchaseInvoices : DevExpress.XtraEditors.XtraUserControl
    {
        public UcPurchaseInvoices()
        {
            InitializeComponent();
            gridView1.CustomColumnDisplayText += (s, e) =>
            {
                if (e.Column.FieldName == "Status")
                    e.DisplayText = LocalizationManager.TranslateStatusCode(e.Value as string);
            };
            Sett.FixCellTooltips(gridView1);
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.OptionsBehavior.Editable = false;
            Sett.CenterColumns(gridView1);
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            ColSupplier.Caption = LocalizationManager.T("Purchases_ColSupplier");
            ColBranch.Caption = LocalizationManager.T("Shared_Branch");
            ColInvoiceDate.Caption = LocalizationManager.T("Purchases_ColInvoiceDate");
            ColTotalAmount.Caption = LocalizationManager.T("Shared_TotalAmount");
            ColPaidAmount.Caption = LocalizationManager.T("Purchases_ColPaidAmount");
            ColStatus.Caption = LocalizationManager.T("Shared_Status");
        }

        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.PurchaseInvoices
                    .Include(x => x.Supplier)
                    .Include(x => x.Branch)
                    .Where(x => !PermissionManager.BranchRestricted || x.BranchId == FrmLogin.CurrentBranchId)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Select(x => new
                    {
                        x.Id,
                        Supplier = x.Supplier.Name,
                        Branch = x.Branch.Name,
                        x.InvoiceDate,
                        x.TotalAmount,
                        x.PaidAmount,
                        x.Status
                    })
                    .ToList();
            }
        }
        private void UcPurchaseInvoices_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
         
        }
        private void AddNew()
        {
            var form = new FrmPurchaseInvoiceEdit(LocalizationManager.T("Purchases_NewInvoiceTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    decimal total = form.Lines.Sum(l => l.Total);
                    decimal paidNow = form.PaidNow;
                    string status = paidNow >= total ? "Completed" : "Pending";

                    var invoice = new PurchaseInvoiceEntity
                    {
                        SupplierId = form.SupplierId,
                        BranchId = form.BranchId,
                        InvoiceDate = DateTime.Now,
                        TotalAmount = total,
                        PaidAmount = paidNow,
                        Status = status,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    };
                    db.PurchaseInvoices.Add(invoice);
                    db.SaveChanges();

                    foreach (var line in form.Lines)
                    {
                        db.PurchaseInvoiceDetails.Add(new PurchaseInvoiceDetailEntity
                        {
                            PurchaseInvoiceId = invoice.Id,
                            ProductVariantId = line.ProductVariantId,
                            Quantity = line.Quantity,
                            UnitCost = line.UnitCost,
                            Total = line.Total
                        });

                        var stock = db.BranchStock.FirstOrDefault(s =>
                            s.ProductVariantId == line.ProductVariantId && s.BranchId == form.BranchId);

                        if (stock == null)
                        {
                            db.BranchStock.Add(new BranchStockEntity
                            {
                                ProductVariantId = line.ProductVariantId,
                                BranchId = form.BranchId,
                                Quantity = line.Quantity,
                                MinQuantity = 0
                            });
                        }
                        else
                        {
                            stock.Quantity += line.Quantity;
                        }

                        // Keep the variant's official cost price in sync with the latest purchase price
                        var variant = db.ProductVariants.FirstOrDefault(v => v.Id == line.ProductVariantId);
                        if (variant != null)
                            variant.CostPrice = line.UnitCost;

                        db.StockMovements.Add(new StockMovementEntity
                        {
                            ProductVariantId = line.ProductVariantId,
                            BranchId = form.BranchId,
                            MovementType = "Purchase",
                            Quantity = line.Quantity,
                            RefType = "PurchaseInvoice",
                            RefId = invoice.Id,
                            CreatedAt = DateTime.Now,
                            CreatedByUserId = FrmLogin.CurrentUserId
                        });
                    }

                    // Record the cash actually paid to the supplier right now (if any)
                    if (paidNow > 0)
                    {
                        db.TreasuryTransactions.Add(new TreasuryEntity
                        {
                            BranchId = form.BranchId,
                            TransactionType = "Out",
                            Amount = paidNow,
                            Description = $"Payment to supplier - Invoice #{invoice.Id}",
                            RefType = "PurchaseInvoice",
                            RefId = invoice.Id,
                            CreatedAt = DateTime.Now,
                            CreatedByUserId = FrmLogin.CurrentUserId
                        });
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    string statusMsg = status == "Completed" ? LocalizationManager.T("Purchases_FullyPaid") : string.Format(LocalizationManager.T("Purchases_PartiallyPaidFmt"), paidNow, total);
                    Sett.MsgBlue(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Purchases_SavedStatus"), statusMsg));
                    GetData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Purchases_SaveFailed"), ex.Message));
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
            if (PermissionManager.CanEdit("PurchaseInvoices")) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow && PermissionManager.CanEdit("PurchaseInvoices"))
            {
                decimal total = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("TotalAmount"));
                decimal paid = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("PaidAmount"));
                if (paid < total)
                    menu.Items.Add(LocalizationManager.T("Payment_MenuCompletePayment"), null, (s, ev) => CompletePayment());
            }
            menu.Items.Add(LocalizationManager.T("Shared_MenuExport"), null, (s, ev) => Sett.ExportGrid(gridControl1, LocalizationManager.T("Main_PurchaseInvoices")));
        }

        private void CompletePayment()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            PurchaseInvoiceEntity invoice;
            using (var db = new ClothesShopDBContext())
                invoice = db.PurchaseInvoices.FirstOrDefault(x => x.Id == id);
            if (invoice == null) return;

            if (invoice.PaidAmount >= invoice.TotalAmount)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_Info"), LocalizationManager.T("Payment_AlreadyFullyPaid"));
                return;
            }

            var form = new FrmCompletePayment(LocalizationManager.T("Payment_CompletePaymentTitle"), invoice.TotalAmount, invoice.PaidAmount);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var dbInvoice = db.PurchaseInvoices.FirstOrDefault(x => x.Id == id);
                    if (dbInvoice == null) return;

                    dbInvoice.PaidAmount += form.AmountToPay;
                    dbInvoice.Status = dbInvoice.PaidAmount >= dbInvoice.TotalAmount ? "Completed" : "Pending";

                    // A new, separate Treasury entry for just this payment - same
                    // convention as the original creation-time entry and as Returns:
                    // never mutate a past Treasury row, only ever add new ones.
                    db.TreasuryTransactions.Add(new TreasuryEntity
                    {
                        BranchId = dbInvoice.BranchId,
                        TransactionType = "Out",
                        Amount = form.AmountToPay,
                        Description = $"Payment to supplier - Invoice #{dbInvoice.Id}",
                        RefType = "PurchaseInvoice",
                        RefId = dbInvoice.Id,
                        CreatedAt = DateTime.Now,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    });

                    db.SaveChanges();
                    transaction.Commit();

                    Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Payment_Recorded"));
                    GetData();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), ex.Message);
                }
            }
        }
    }
}
