using Clothes_Shop_ERP.DAL;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    // شاشة قراءة فقط لجدول AuditLogs (مفيش Add/Edit/Delete هنا، بس عرض وفلترة)
    public class UcAuditLogs : DevExpress.XtraEditors.XtraUserControl
    {
        private DateEdit DtFrom, DtTo;
        private ComboBoxEdit CmbTable;
        private GridControl GridResult;
        private GridView GridViewResult;

        public UcAuditLogs()
        {
            this.Dock = DockStyle.Fill;
            BuildUi();
            RunReport();
        }

        private void BuildUi()
        {
            var lblFrom = new LabelControl { Text = "من:", Location = new Point(20, 18) };
            DtFrom = new DateEdit { Location = new Point(20, 38), Width = 140 };
            DtFrom.DateTime = DateTime.Today.AddDays(-7);

            var lblTo = new LabelControl { Text = "إلى:", Location = new Point(170, 18) };
            DtTo = new DateEdit { Location = new Point(170, 38), Width = 140 };
            DtTo.DateTime = DateTime.Today;

            var lblTable = new LabelControl { Text = "الجدول:", Location = new Point(320, 18) };
            CmbTable = new ComboBoxEdit { Location = new Point(320, 38), Width = 200 };
            CmbTable.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

            var btnRun = new SimpleButton { Text = "تحديث", Location = new Point(530, 37), Width = 100 };
            btnRun.Click += (s, e) => RunReport();

            GridResult = new GridControl
            {
                Location = new Point(20, 75),
                Size = new Size(900, 450),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            GridViewResult = new GridView(GridResult);
            GridResult.MainView = GridViewResult;
            GridViewResult.OptionsBehavior.Editable = false;
            GridViewResult.OptionsView.ShowGroupPanel = false;
            GridViewResult.RowCellStyle += GridViewResult_RowCellStyle;

            this.Controls.Add(lblFrom); this.Controls.Add(DtFrom);
            this.Controls.Add(lblTo); this.Controls.Add(DtTo);
            this.Controls.Add(lblTable); this.Controls.Add(CmbTable);
            this.Controls.Add(btnRun);
            this.Controls.Add(GridResult);
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
                                User = u != null ? u.FullName : "System"
                            };

                var data = query.ToList();

                // فلتر اسم الجدول (لو المستخدم اختار جدول معين من الكومبو)
                if (CmbTable.SelectedIndex > 0)
                {
                    string selectedTable = CmbTable.Text;
                    data = data.Where(x => x.TableName == selectedTable).ToList();
                }

                GridResult.DataSource = data;

                // تعبئة الكومبو بأسماء الجداول الموجودة فعليًا (مرة واحدة بس لو فاضي)
                if (CmbTable.Properties.Items.Count == 0)
                {
                    CmbTable.Properties.Items.Add("الكل");
                    foreach (var t in db.AuditLogs.Select(x => x.TableName).Distinct().OrderBy(x => x))
                        CmbTable.Properties.Items.Add(t);
                    CmbTable.SelectedIndex = 0;
                }
            }
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "UcAuditLogs";
            this.Size = new Size(950, 550);
            this.ResumeLayout(false);
        }
    }
}