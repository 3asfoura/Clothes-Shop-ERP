using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcAuditLogs : DevExpress.XtraEditors.XtraUserControl
    {
        public UcAuditLogs()
        {
            InitializeComponent();
            DtFrom.DateTime = DateTime.Today.AddDays(-7);
            DtTo.DateTime = DateTime.Today;
            ApplyLanguage();
            RunReport();
        }

        public void ApplyLanguage()
        {
            lblFrom.Text = LocalizationManager.T("Shared_From");
            lblTo.Text = LocalizationManager.T("Shared_To");
            lblTable.Text = LocalizationManager.T("AuditLogs_Table");
            btnRun.Text = LocalizationManager.T("Shared_Refresh");
        }

        private void RunReport()
        {
            DateTime fromDate = DtFrom.DateTime.Date;
            DateTime toDate = DtTo.DateTime.Date.AddDays(1).AddSeconds(-1);

            using (var db = new ClothesShopDBContext())
            {
                var query = from log in db.AuditLogs
                            join u in db.Users on log.ChangedByUserId equals u.Id into users
                            from u in users.DefaultIfEmpty()
                            where log.ChangedAt >= fromDate && log.ChangedAt <= toDate
                            orderby log.ChangedAt descending
                            select new
                            {
                                log.ChangedAt,
                                log.TableName,
                                log.RecordId,
                                log.Action,
                                User = u != null ? u.FullName : LocalizationManager.T("AuditLogs_SystemUser")
                            };

                var data = query.ToList();

                if (CmbTable.SelectedIndex > 0)
                {
                    string selectedTable = CmbTable.Text;
                    data = data.Where(x => x.TableName == selectedTable).ToList();
                }

                GridResult.DataSource = data;
                GridViewResult.PopulateColumns();
                if (GridViewResult.Columns["ChangedAt"] != null) GridViewResult.Columns["ChangedAt"].Caption = LocalizationManager.T("AuditLogs_ColChangedAt");
                if (GridViewResult.Columns["TableName"] != null) GridViewResult.Columns["TableName"].Caption = LocalizationManager.T("AuditLogs_ColTable");
                if (GridViewResult.Columns["RecordId"] != null) GridViewResult.Columns["RecordId"].Caption = LocalizationManager.T("AuditLogs_ColRecordId");
                if (GridViewResult.Columns["Action"] != null) GridViewResult.Columns["Action"].Caption = LocalizationManager.T("AuditLogs_ColAction");
                if (GridViewResult.Columns["User"] != null) GridViewResult.Columns["User"].Caption = LocalizationManager.T("AuditLogs_ColUser");
                Sett.CenterColumns(GridViewResult);

                if (CmbTable.Properties.Items.Count == 0)
                {
                    CmbTable.Properties.Items.Add(LocalizationManager.T("txtAll"));
                    foreach (var t in db.AuditLogs.Select(x => x.TableName).Distinct().OrderBy(x => x))
                        CmbTable.Properties.Items.Add(t);
                    CmbTable.SelectedIndex = 0;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }

        private void GridViewResult_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "Action") return;
            var row = GridViewResult.GetRow(e.RowHandle);
            var actionProp = row.GetType().GetProperty("Action");
            string action = actionProp?.GetValue(row) as string;

            if (action == "Insert") { e.Appearance.ForeColor = Color.DarkGreen; }
            else if (action == "Update") { e.Appearance.ForeColor = Color.DarkBlue; }
            else if (action == "Delete") { e.Appearance.ForeColor = Color.DarkRed; }
        }
    }
}
