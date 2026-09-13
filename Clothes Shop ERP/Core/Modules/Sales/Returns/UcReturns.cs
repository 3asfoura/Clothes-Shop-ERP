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
using Microsoft.EntityFrameworkCore;
using SalesReturnEntity = Clothes_Shop_ERP.DAL.SalesReturns;
using SalesReturnDetailEntity = Clothes_Shop_ERP.DAL.SalesReturnDetails; 
using StockMovementEntity = Clothes_Shop_ERP.DAL.StockMovements;
using TreasuryEntity = Clothes_Shop_ERP.DAL.TreasuryTransactions;
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcReturns : DevExpress.XtraEditors.XtraUserControl
    {
        public UcReturns()
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
            Col.Caption = LocalizationManager.T("Returns_ColInvoice");
            ColBranch.Caption = LocalizationManager.T("Shared_Branch");
            ColReturnDate.Caption = LocalizationManager.T("Returns_ColReturnDate");
            ColTotalAmount.Caption = LocalizationManager.T("Shared_TotalAmount");
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.SalesReturns
                    .Include(x => x.SalesInvoice)
                    .Include(x => x.Branch)
                    .OrderByDescending(x => x.ReturnDate)
                    .Select(x => new
                    {
                        x.Id,
                        Invoice = x.SalesInvoice.InvoiceNumber,
                        Branch = x.Branch.Name,
                        x.ReturnDate,
                        x.TotalAmount
                    })
                    .ToList();
            }
        }

        private void AddNew()
        {
            var form = new FrmReturnEdit(LocalizationManager.T("Returns_NewTitle"));
            if (form.ShowDialog() != DialogResult.OK) return;

            int branchId = FrmLogin.CurrentBranchId;
            // Several items from the same invoice can now come back in one go, so this
            // is one return header with a detail row (and its own stock movement) per
            // item, rather than a whole separate return per item.
            var lines = form.Lines;
            decimal total = lines.Sum(l => l.UnitPrice * l.ReturnQty);

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var salesReturn = new SalesReturnEntity
                    {
                        SalesInvoiceId = form.SalesInvoiceId,
                        BranchId = branchId,
                        ReturnDate = DateTime.Now,
                        TotalAmount = total,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    };
                    db.SalesReturns.Add(salesReturn);
                    db.SaveChanges();   // generates salesReturn.Id for the detail rows below

                    foreach (var line in lines)
                    {
                        decimal lineTotal = line.UnitPrice * line.ReturnQty;

                        db.SalesReturnDetails.Add(new SalesReturnDetailEntity
                        {
                            SalesReturnId = salesReturn.Id,
                            ProductVariantId = line.ProductVariantId,
                            Quantity = line.ReturnQty,
                            UnitPrice = line.UnitPrice,
                            Total = lineTotal
                        });

                        // Give the stock back
                        var stock = db.BranchStock.FirstOrDefault(s =>
                            s.ProductVariantId == line.ProductVariantId && s.BranchId == branchId);

                        if (stock == null)
                        {
                            db.BranchStock.Add(new Clothes_Shop_ERP.DAL.BranchStock
                            {
                                ProductVariantId = line.ProductVariantId,
                                BranchId = branchId,
                                Quantity = line.ReturnQty,
                                MinQuantity = 0
                            });
                        }
                        else
                        {
                            stock.Quantity += line.ReturnQty;
                        }

                        db.StockMovements.Add(new StockMovementEntity
                        {
                            ProductVariantId = line.ProductVariantId,
                            BranchId = branchId,
                            MovementType = "Return",
                            Quantity = line.ReturnQty,
                            RefType = "SalesReturn",
                            RefId = salesReturn.Id,
                            CreatedAt = DateTime.Now,
                            CreatedByUserId = FrmLogin.CurrentUserId
                        });
                    }

                    // Money goes back out of the till - one entry for the whole return
                    db.TreasuryTransactions.Add(new TreasuryEntity
                    {
                        BranchId = branchId,
                        TransactionType = "Out",
                        Amount = total,
                        Description = "Sales return",
                        RefType = "SalesReturn",
                        RefId = salesReturn.Id,
                        CreatedAt = DateTime.Now,
                        CreatedByUserId = FrmLogin.CurrentUserId
                    });

                    db.SaveChanges();
                    transaction.Commit();

                    Sett.MsgGreen(LocalizationManager.T("Shared_Success"), string.Format(LocalizationManager.T("Returns_Recorded"), total));
                    GetData();
                }
                catch (Exception ex)
                {
                    ErrorReporter.Log(ex, "Sales return - save");
                    transaction.Rollback();
                    Sett.MsgBlue(LocalizationManager.T("Shared_Error"), string.Format(LocalizationManager.T("Returns_SaveFailed"), ex.Message));
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
            if (PermissionManager.CanEdit("Returns")) menu.Items.Add(LocalizationManager.T("Shared_MenuNew"), null, (s, ev) => AddNew());
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {

            }
            menu.Items.Add(LocalizationManager.T("Shared_MenuExport"), null, (s, ev) => Sett.ExportGrid(gridControl1, LocalizationManager.T("Main_Returns")));
        }
    }
}
