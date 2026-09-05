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
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
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

            if (hit.InRow)
            {
                //menu.Items.Add("Edit", null, (s, ev) => EditSelected());
                //menu.Items.Add("Activate/Deactivate", null, (s, ev) => ToggleActive());
                //menu.Items.Add("Delete", null, (s, ev) => DeleteSelected());
            }
        }
    }
}
