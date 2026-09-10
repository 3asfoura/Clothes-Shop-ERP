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

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcSalesInvoices : DevExpress.XtraEditors.XtraUserControl
    {
        public UcSalesInvoices()
        {
            InitializeComponent();
            gridView1.CustomColumnDisplayText += (s, e) =>
            {
                if (e.Column.FieldName == "Status")
                    e.DisplayText = LocalizationManager.TranslateStatusCode(e.Value as string);
            };
            Sett.FixCellTooltips(gridView1);
            GetData();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.OptionsBehavior.Editable = false;
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.SalesInvoices
                    .Include(x => x.Customer)
                    .Include(x => x.Branch)
                    .Where(x => !PermissionManager.BranchRestricted || x.BranchId == FrmLogin.CurrentBranchId)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Select(x => new
                    {
                        x.Id,
                        x.InvoiceNumber,
                        Customer = x.Customer != null ? x.Customer.Name : LocalizationManager.T("SalesInvoices_WalkInCustomer"),
                        Branch = x.Branch.Name,
                        x.InvoiceDate,
                        x.NetAmount,
                        x.PaidAmount,
                        x.Status
                    })
                    .ToList();
            }
            gridView1.PopulateColumns();
            if (gridView1.Columns["InvoiceNumber"] != null) gridView1.Columns["InvoiceNumber"].Caption = LocalizationManager.T("SalesInvoices_ColInvoiceNumber");
            if (gridView1.Columns["Customer"] != null) gridView1.Columns["Customer"].Caption = LocalizationManager.T("SalesInvoices_ColCustomer");
            if (gridView1.Columns["Branch"] != null) gridView1.Columns["Branch"].Caption = LocalizationManager.T("Shared_Branch");
            if (gridView1.Columns["InvoiceDate"] != null) gridView1.Columns["InvoiceDate"].Caption = LocalizationManager.T("Purchases_ColInvoiceDate");
            if (gridView1.Columns["NetAmount"] != null) gridView1.Columns["NetAmount"].Caption = LocalizationManager.T("SalesInvoices_ColNetAmount");
            if (gridView1.Columns["PaidAmount"] != null) gridView1.Columns["PaidAmount"].Caption = LocalizationManager.T("Purchases_ColPaidAmount");
            if (gridView1.Columns["Status"] != null) gridView1.Columns["Status"].Caption = LocalizationManager.T("Shared_Status");
            // PopulateColumns() above rebuilds the whole Columns collection from
            // scratch every time GetData() runs, wiping any previously-applied
            // alignment - so centering has to be re-applied here, not just once
            // in the constructor.
            Sett.CenterColumns(gridView1);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hit = gridView1.CalcHitInfo(e.Location);
            if (hit.InRow)
                gridView1.FocusedRowHandle = hit.RowHandle;

            var menu = new ContextMenuStrip();
           
            menu.Show(gridControl1, e.Location);

            if (hit.InRow)
            {
                menu.Items.Add(LocalizationManager.T("Shared_MenuViewDetails"), null, (s, ev) => ViewDetails());
                menu.Items.Add(LocalizationManager.T("Shared_MenuPrintReceipt"), null, (s, ev) => PrintReceipt());

                if (PermissionManager.CanEdit("SalesInvoices"))
                {
                    decimal net = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("NetAmount"));
                    decimal paid = Convert.ToDecimal(gridView1.GetFocusedRowCellValue("PaidAmount"));
                    if (paid < net)
                        menu.Items.Add(LocalizationManager.T("Payment_MenuCompletePayment"), null, (s, ev) => CompletePayment());
                }
            }
            menu.Items.Add(LocalizationManager.T("Shared_MenuExport"), null, (s, ev) => Sett.ExportGrid(gridControl1, LocalizationManager.T("Main_SalesInvoices")));
        }

        private void CompletePayment()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            SalesInvoices invoice;
            using (var db = new ClothesShopDBContext())
                invoice = db.SalesInvoices.FirstOrDefault(x => x.Id == id);
            if (invoice == null) return;

            if (invoice.PaidAmount >= invoice.NetAmount)
            {
                Sett.MsgBlue(LocalizationManager.T("Shared_Info"), LocalizationManager.T("Payment_AlreadyFullyPaid"));
                return;
            }

            var form = new FrmCompletePayment(LocalizationManager.T("Payment_CompletePaymentTitle"), invoice.NetAmount, invoice.PaidAmount);
            if (form.ShowDialog() != DialogResult.OK) return;

            using (var db = new ClothesShopDBContext())
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var dbInvoice = db.SalesInvoices.FirstOrDefault(x => x.Id == id);
                    if (dbInvoice == null) return;

                    dbInvoice.PaidAmount += form.AmountToPay;
                    dbInvoice.Status = dbInvoice.PaidAmount >= dbInvoice.NetAmount ? "Completed" : "Pending";

                    // A new, separate Treasury entry for just this payment - same
                    // convention as POS checkout and as Returns: never mutate a
                    // past Treasury row, only ever add new ones.
                    db.TreasuryTransactions.Add(new TreasuryTransactions
                    {
                        BranchId = dbInvoice.BranchId,
                        TransactionType = "In",
                        Amount = form.AmountToPay,
                        Description = $"Sale - {dbInvoice.InvoiceNumber}",
                        RefType = "SalesInvoice",
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
        private void PrintReceipt()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            using (var db = new ClothesShopDBContext())
            {
                var invoice = db.SalesInvoices
                    .Include(x => x.Customer)
                    .Include(x => x.Branch)
                    .Include(x => x.PaymentMethod)
                    .FirstOrDefault(x => x.Id == id);
                if (invoice == null) return;

                var details = db.SalesInvoiceDetails
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Where(x => x.SalesInvoiceId == id)
                    .ToList();

                var receipt = new ReceiptData
                {
                    ShopName = invoice.Branch?.Name,
                    ShopAddress = invoice.Branch?.Address,
                    ShopPhone = invoice.Branch?.Phone,
                    InvoiceNumber = invoice.InvoiceNumber,
                    Date = invoice.InvoiceDate,
                    Customer = invoice.Customer?.Name ?? LocalizationManager.T("SalesInvoices_WalkInCustomer"),
                    PaymentMethod = invoice.PaymentMethod?.Name,
                    SubTotal = invoice.TotalAmount,
                    Discount = invoice.DiscountAmount,
                    NetTotal = invoice.NetAmount,
                    Lines = details.Select(d => new ReceiptLine
                    {
                        Product = $"{d.ProductVariant.Product.Name} ({d.ProductVariant.Barcode})",
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        LineTotal = d.Total
                    }).ToList()
                };

                ReceiptPrinter.Preview(receipt);
            }
        }
        private void ViewDetails()
        {
            if (gridView1.FocusedRowHandle < 0) return;
            int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Id"));

            using (var db = new ClothesShopDBContext())
            {
                var details = db.SalesInvoiceDetails
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Where(x => x.SalesInvoiceId == id)
                    .ToList();

                string message = string.Join("\n", details.Select(d =>
                    $"{d.ProductVariant.Product.Name} ({d.ProductVariant.Barcode})  x{d.Quantity}  @ {d.UnitPrice:n2} = {d.Total:n2}"));

                XtraMessageBox.Show(message, LocalizationManager.T("SalesInvoices_DetailsTitle"));
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Row) return;
            if (e.HitInfo.InRow) gridView1.FocusedRowHandle = e.HitInfo.RowHandle;

            e.Menu.Items.Clear();
            if (e.HitInfo.InRow)
            {
                e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem(LocalizationManager.T("Shared_MenuViewDetails"), (s, ev) => ViewDetails()));
            }
        }
    }
}
