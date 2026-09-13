using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // One line of a stock count: what the system thinks is on the shelf versus what
    // was actually counted. Counted is deliberately nullable - blank means "nobody
    // has counted this item yet", which is NOT the same as counting it and finding
    // zero, and only the ones that were actually counted get adjusted on save.
    public class StockCountLine
    {
        public int ProductVariantId { get; set; }
        public string Product { get; set; }
        public string Barcode { get; set; }
        public decimal SystemQty { get; set; }
        public decimal? Counted { get; set; }
        public decimal? Difference => Counted.HasValue ? Counted.Value - SystemQty : (decimal?)null;
    }

    // Scan-driven stock count: scan each physical item and the matching row's count
    // goes up by one, instead of opening a form per item and retyping quantities.
    public partial class FrmStockCountSession : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>Rows somebody actually counted - everything else is left alone.</summary>
        public List<StockCountLine> CountedLines => _lines.Where(l => l.Counted.HasValue).ToList();

        private TextEdit TxtBarcode;
        private LabelControl LblSummary;
        private GridControl GridLines;
        private GridView GridViewLines;
        private BindingList<StockCountLine> _lines = new BindingList<StockCountLine>();

        public FrmStockCountSession(string title)
        {
            this.Text = title;
            this.Width = 760;
            this.Height = 560;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblScan = new LabelControl { Text = LocalizationManager.T("StockCountSession_ScanHint"), Location = new System.Drawing.Point(20, 20) };
            TxtBarcode = new TextEdit { Location = new System.Drawing.Point(20, 40), Width = 400 };

            GridLines = new GridControl { Location = new System.Drawing.Point(20, 80), Size = new System.Drawing.Size(700, 340) };
            GridViewLines = new GridView(GridLines);
            GridLines.MainView = GridViewLines;
            // Off BEFORE binding - otherwise DevExpress generates its own columns from
            // the property names and regenerates them whenever the data changes, wiping
            // the translated captions set below.
            GridViewLines.OptionsBehavior.AutoPopulateColumns = false;
            GridLines.DataSource = _lines;
            GridViewLines.OptionsView.ShowGroupPanel = false;
            GridViewLines.OptionsBehavior.Editable = true;
            ConfigureColumns();

            // Difference is computed from Counted, so the grid has to be told to redraw
            // after a quantity is typed by hand - it only tracks the edited cell itself.
            GridViewLines.CellValueChanged += (s, e) => { GridViewLines.RefreshData(); RefreshSummary(); };

            LblSummary = new LabelControl
            {
                Location = new System.Drawing.Point(20, 430),
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
            };

            var btnApply = new SimpleButton { Text = LocalizationManager.T("StockCountSession_BtnApply"), Location = new System.Drawing.Point(480, 470), Width = 130, DialogResult = DialogResult.OK };
            btnApply.Click += (s, e) =>
            {
                // Commits the cell still being edited, so a quantity typed and left
                // without pressing Enter isn't silently dropped.
                GridViewLines.PostEditor();
                GridViewLines.UpdateCurrentRow();

                if (CountedLines.Count == 0)
                {
                    XtraMessageBox.Show(LocalizationManager.T("StockCountSession_NothingCounted"));
                    this.DialogResult = DialogResult.None;
                }
            };

            var btnCancel = new SimpleButton { Text = LocalizationManager.T("Shared_BtnCancel"), Location = new System.Drawing.Point(620, 470), Width = 100, DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblScan); this.Controls.Add(TxtBarcode);
            this.Controls.Add(GridLines);
            this.Controls.Add(LblSummary);
            this.Controls.Add(btnApply); this.Controls.Add(btnCancel);

            this.CancelButton = btnCancel;
            this.ActiveControl = TxtBarcode;   // ready to scan the moment it opens

            LoadCurrentStock();
            RefreshSummary();
        }

        // A single-line TextEdit doesn't claim the Enter key, so Windows would hand it
        // to this dialog's default button before the editor ever sees it. Intercepting
        // here keeps a scan from submitting the whole count.
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && TxtBarcode != null && TxtBarcode.Focused)
            {
                ProcessScan();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ConfigureColumns()
        {
            var colProduct = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Product",
                Caption = LocalizationManager.T("StockCount_ColProduct"),
                Visible = true,
                VisibleIndex = 0,
                Width = 260
            };
            colProduct.OptionsColumn.AllowEdit = false;

            var colBarcode = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Barcode",
                Caption = LocalizationManager.T("ProductVariants_ColBarcode"),
                Visible = true,
                VisibleIndex = 1
            };
            colBarcode.OptionsColumn.AllowEdit = false;

            var colSystem = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "SystemQty",
                Caption = LocalizationManager.T("StockCountSession_ColSystem"),
                Visible = true,
                VisibleIndex = 2
            };
            colSystem.OptionsColumn.AllowEdit = false;
            colSystem.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colSystem.DisplayFormat.FormatString = "0.###";

            // Blank (null) is a meaningful state here - "not counted yet" - so the
            // editor has to accept an empty value, not force it to 0.
            var countedEditor = new RepositoryItemSpinEdit();
            countedEditor.MinValue = 0;
            countedEditor.MaxValue = 999999;
            countedEditor.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            countedEditor.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            countedEditor.DisplayFormat.FormatString = "0.###";
            GridLines.RepositoryItems.Add(countedEditor);

            var colCounted = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Counted",
                Caption = LocalizationManager.T("StockCountSession_ColCounted"),
                Visible = true,
                VisibleIndex = 3,
                ColumnEdit = countedEditor
            };

            var colDifference = new DevExpress.XtraGrid.Columns.GridColumn
            {
                FieldName = "Difference",
                Caption = LocalizationManager.T("StockCountSession_ColDifference"),
                Visible = true,
                VisibleIndex = 4
            };
            colDifference.OptionsColumn.AllowEdit = false;
            colDifference.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colDifference.DisplayFormat.FormatString = "0.###";

            GridViewLines.Columns.AddRange(new[] { colProduct, colBarcode, colSystem, colCounted, colDifference });
        }

        // Everything this branch currently holds, so the count starts from the real
        // shelf list and anything left blank is visibly "not counted".
        private void LoadCurrentStock()
        {
            _lines.Clear();
            using (var db = new ClothesShopDBContext())
            {
                var rows = db.BranchStock
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Where(x => x.BranchId == FrmLogin.CurrentBranchId)
                    .ToList();

                foreach (var r in rows)
                {
                    _lines.Add(new StockCountLine
                    {
                        ProductVariantId = r.ProductVariantId,
                        Product = r.ProductVariant.Product.Name,
                        Barcode = r.ProductVariant.Barcode,
                        SystemQty = r.Quantity,
                        Counted = null
                    });
                }
            }
        }

        private void ProcessScan()
        {
            string input = TxtBarcode.Text.Trim();
            TxtBarcode.Text = "";
            if (string.IsNullOrEmpty(input)) return;

            // Same "qty*barcode" shorthand the POS screen uses, so counting 12 of
            // something is one scan plus a number rather than twelve scans.
            string code = input;
            decimal quantity = 1;
            int starIndex = input.IndexOf('*');
            if (starIndex > 0)
            {
                string qtyPart = input.Substring(0, starIndex);
                string codePart = input.Substring(starIndex + 1);
                if (decimal.TryParse(qtyPart, out decimal parsedQty) && parsedQty > 0 && codePart.Length > 0)
                {
                    quantity = parsedQty;
                    code = codePart;
                }
            }

            var line = _lines.FirstOrDefault(l => l.Barcode == code);
            if (line == null)
            {
                // Not on the shelf list: either genuinely new to this branch (system
                // quantity 0) or not a Belnix barcode at all.
                using (var db = new ClothesShopDBContext())
                {
                    var variant = db.ProductVariants
                        .Include(x => x.Product)
                        .FirstOrDefault(v => v.Barcode == code && v.IsActive == true && v.Product.IsActive == true);

                    if (variant == null)
                    {
                        Sett.MsgBlue(LocalizationManager.T("POS_NotFoundTitle"), string.Format(LocalizationManager.T("POS_ProductNotFoundByBarcode"), code));
                        return;
                    }

                    line = new StockCountLine
                    {
                        ProductVariantId = variant.Id,
                        Product = variant.Product.Name,
                        Barcode = variant.Barcode,
                        SystemQty = 0,
                        Counted = null
                    };
                    _lines.Insert(0, line);   // straight to the top, it's what was just scanned
                }
            }

            line.Counted = (line.Counted ?? 0) + quantity;

            GridViewLines.RefreshData();
            int rowHandle = GridViewLines.GetRowHandle(_lines.IndexOf(line));
            if (rowHandle >= 0)
            {
                GridViewLines.FocusedRowHandle = rowHandle;
                GridViewLines.MakeRowVisible(rowHandle);
            }
            RefreshSummary();
        }

        private void RefreshSummary()
        {
            int counted = _lines.Count(l => l.Counted.HasValue);
            int differing = _lines.Count(l => l.Counted.HasValue && l.Counted.Value != l.SystemQty);
            LblSummary.Text = string.Format(LocalizationManager.T("StockCountSession_SummaryFmt"), counted, _lines.Count, differing);
        }
    }
}
