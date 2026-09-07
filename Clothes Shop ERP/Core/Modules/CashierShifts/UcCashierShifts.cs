using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CashierShiftEntity = Clothes_Shop_ERP.DAL.CashierShifts;

namespace Clothes_Shop_ERP.modlestore
{
    // Lets a cashier open a shift with a starting cash float, then close it at
    // the end of the day by counting the drawer. "Expected cash" is computed
    // from what's actually known to be cash: cash-flagged sales made by this
    // user at this branch since the shift opened, minus cash-flagged returns,
    // plus/minus manual treasury cash movements they made themselves. Purchase
    // invoice payments aren't included since purchases don't track payment
    // method - see the note in ComputeExpectedCash.
    public partial class UcCashierShifts : DevExpress.XtraEditors.XtraUserControl
    {
        public UcCashierShifts()
        {
            InitializeComponent();
            BtnOpen.Text = LocalizationManager.T("Shift_BtnOpen");
            BtnClose.Text = LocalizationManager.T("Shift_BtnClose");
            BtnOpen.Click += (s, e) => OpenShift();
            BtnClose.Click += (s, e) => CloseShift();
            bool canEdit = PermissionManager.CanEdit("CashierShifts");
            BtnOpen.Enabled = canEdit;
            BtnClose.Enabled = canEdit;
            GridResult.MouseUp += (s, e) =>
            {
                if (e.Button != MouseButtons.Right) return;
                var menu = new ContextMenuStrip();
                menu.Items.Add(LocalizationManager.T("Shared_MenuExport"), null, (s2, ev) => Sett.ExportGrid(GridResult, LocalizationManager.T("Main_CashierShifts")));
                menu.Show(GridResult, e.Location);
            };
            GridViewResult.CustomColumnDisplayText += (s, e) =>
            {
                if (e.Column.FieldName == "Status")
                    e.DisplayText = LocalizationManager.TranslateStatusCode(e.Value as string);
            };
            Sett.FixCellTooltips(GridViewResult);
            RefreshStatus();
            GetData();
        }

        private CashierShiftEntity GetMyOpenShift(ClothesShopDBContext db)
        {
            return db.CashierShifts.FirstOrDefault(x => x.UserId == FrmLogin.CurrentUserId && x.Status == "Open");
        }

        private void RefreshStatus()
        {
            using (var db = new ClothesShopDBContext())
            {
                var open = GetMyOpenShift(db);
                bool canEdit = PermissionManager.CanEdit("CashierShifts");

                if (open == null)
                {
                    LblStatus.Text = LocalizationManager.T("Shift_NoOpenShift");
                    LblStatus.AppearanceItemCaption.ForeColor = Color.DarkRed;
                    LblStatus.AppearanceItemCaption.Options.UseForeColor = true;
                    BtnOpen.Enabled = canEdit;
                    BtnClose.Enabled = false;
                }
                else
                {
                    LblStatus.Text = string.Format(LocalizationManager.T("Shift_OpenSinceFmt"), open.OpenedAt, open.OpeningFloat);
                    LblStatus.AppearanceItemCaption.ForeColor = Color.DarkGreen;
                    LblStatus.AppearanceItemCaption.Options.UseForeColor = true;
                    BtnOpen.Enabled = false;
                    BtnClose.Enabled = canEdit;
                }
            }
        }

        // Only counts cash movements we can actually attribute reliably: cash
        // sales/returns by this cashier, and manual treasury entries they made
        // themselves. Cash paid to suppliers via Purchase Invoices is excluded
        // because that flow doesn't record a payment method at all.
        private decimal ComputeExpectedCash(ClothesShopDBContext db, CashierShiftEntity shift)
        {
            decimal cashSales = db.SalesInvoices
                .Where(x => x.CreatedByUserId == shift.UserId && x.BranchId == shift.BranchId
                         && x.InvoiceDate >= shift.OpenedAt && x.Status == "Completed"
                         && x.PaymentMethod.IsCash)
                .Sum(x => (decimal?)x.NetAmount) ?? 0;

            decimal cashReturns = db.SalesReturns
                .Where(x => x.CreatedByUserId == shift.UserId && x.BranchId == shift.BranchId
                         && x.ReturnDate >= shift.OpenedAt)
                .Sum(x => (decimal?)x.TotalAmount) ?? 0;

            decimal manualCashIn = db.TreasuryTransactions
                .Where(x => x.CreatedByUserId == shift.UserId && x.BranchId == shift.BranchId
                         && x.CreatedAt >= shift.OpenedAt && x.TransactionType == "In" && x.RefType == "Manual")
                .Sum(x => (decimal?)x.Amount) ?? 0;

            decimal manualCashOut = db.TreasuryTransactions
                .Where(x => x.CreatedByUserId == shift.UserId && x.BranchId == shift.BranchId
                         && x.CreatedAt >= shift.OpenedAt && x.TransactionType == "Out" && x.RefType == "Manual")
                .Sum(x => (decimal?)x.Amount) ?? 0;

            return shift.OpeningFloat + cashSales - cashReturns + manualCashIn - manualCashOut;
        }

        private void OpenShift()
        {
            using (var db = new ClothesShopDBContext())
            {
                if (GetMyOpenShift(db) != null)
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Shift_AlreadyOpen"));
                    return;
                }

                var form = new FrmShiftOpen();
                if (form.ShowDialog() != DialogResult.OK) return;

                db.CashierShifts.Add(new CashierShiftEntity
                {
                    BranchId = FrmLogin.CurrentBranchId,
                    UserId = FrmLogin.CurrentUserId,
                    OpenedAt = DateTime.Now,
                    OpeningFloat = form.OpeningFloat,
                    Status = "Open"
                });
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Shift_Opened"));
            RefreshStatus();
            GetData();
        }

        private void CloseShift()
        {
            using (var db = new ClothesShopDBContext())
            {
                var shift = GetMyOpenShift(db);
                if (shift == null)
                {
                    Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Shift_NoOpenShift"));
                    return;
                }

                decimal expected = ComputeExpectedCash(db, shift);
                var form = new FrmShiftClose(expected);
                if (form.ShowDialog() != DialogResult.OK) return;

                shift.ClosedAt = DateTime.Now;
                shift.ExpectedCash = expected;
                shift.CountedCash = form.CountedCash;
                shift.Difference = form.CountedCash - expected;
                shift.Notes = string.IsNullOrWhiteSpace(form.Notes) ? null : form.Notes;
                shift.Status = "Closed";
                db.SaveChanges();
            }
            Sett.MsgBlue(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Shift_Closed"));
            RefreshStatus();
            GetData();
        }

        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                var rows = db.CashierShifts
                    .Include(x => x.User)
                    .Include(x => x.Branch)
                    .Where(x => !PermissionManager.BranchRestricted || x.BranchId == FrmLogin.CurrentBranchId)
                    .OrderByDescending(x => x.OpenedAt)
                    .Select(x => new
                    {
                        Cashier = x.User.FullName,
                        Branch = x.Branch.Name,
                        x.OpenedAt,
                        x.OpeningFloat,
                        x.ClosedAt,
                        x.ExpectedCash,
                        x.CountedCash,
                        x.Difference,
                        x.Status
                    })
                    .ToList();

                GridResult.DataSource = rows;
                GridViewResult.PopulateColumns();
                Sett.CenterColumns(GridViewResult);
                if (GridViewResult.Columns["Cashier"] != null) GridViewResult.Columns["Cashier"].Caption = LocalizationManager.T("Shift_ColCashier");
                if (GridViewResult.Columns["Branch"] != null) GridViewResult.Columns["Branch"].Caption = LocalizationManager.T("Shared_Branch");
                if (GridViewResult.Columns["OpenedAt"] != null) GridViewResult.Columns["OpenedAt"].Caption = LocalizationManager.T("Shift_ColOpenedAt");
                if (GridViewResult.Columns["OpeningFloat"] != null) GridViewResult.Columns["OpeningFloat"].Caption = LocalizationManager.T("Shift_ColOpeningFloat");
                if (GridViewResult.Columns["ClosedAt"] != null) GridViewResult.Columns["ClosedAt"].Caption = LocalizationManager.T("Shift_ColClosedAt");
                if (GridViewResult.Columns["ExpectedCash"] != null) GridViewResult.Columns["ExpectedCash"].Caption = LocalizationManager.T("Shift_ColExpectedCash");
                if (GridViewResult.Columns["CountedCash"] != null) GridViewResult.Columns["CountedCash"].Caption = LocalizationManager.T("Shift_ColCountedCash");
                if (GridViewResult.Columns["Difference"] != null) GridViewResult.Columns["Difference"].Caption = LocalizationManager.T("Shift_ColDifference");
                if (GridViewResult.Columns["Status"] != null) GridViewResult.Columns["Status"].Caption = LocalizationManager.T("Shared_Status");
            }
        }
    }
}
