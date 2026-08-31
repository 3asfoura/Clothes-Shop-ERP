using Clothes_Shop_ERP.DAL;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public class UcStockReport : DevExpress.XtraEditors.XtraUserControl
    {
        private GridControl GridResult;
        private GridView GridViewResult;
        private LabelControl LblSummary;
        public void ApplyLanguage()
        {
            LblSummary.Text = LocalizationManager.T("Reports_Summary");
          
        }
        public UcStockReport()
        {
            this.Dock = DockStyle.Fill;
            BuildUi();
            RunReport();
            ApplyLanguage();
        }

        private void BuildUi()
        {
            var btnRun = new SimpleButton { Text = "Refresh", Location = new System.Drawing.Point(20, 15), Width = 100 };
            btnRun.Click += (s, e) => RunReport();

            GridResult = new GridControl { Location = new System.Drawing.Point(20, 55), Size = new System.Drawing.Size(700, 380) };
            GridViewResult = new GridView(GridResult);
            GridResult.MainView = GridViewResult;
            GridViewResult.OptionsBehavior.Editable = false;
            GridViewResult.RowCellStyle += GridViewResult_RowCellStyle;

            LblSummary = new LabelControl
            {
                Text = "Total Inventory Value: 0.00",
                Location = new System.Drawing.Point(20, 445),
                Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold)
            };

            this.Controls.Add(btnRun);
            this.Controls.Add(GridResult);
            this.Controls.Add(LblSummary);
        }

        private void RunReport()
        {
            using (var db = new ClothesShopDBContext())
            {
                var stock = db.BranchStock
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Include(x => x.Branch)
                    .Select(x => new
                    {
                        Product = x.ProductVariant.Product.Name,
                        Barcode = x.ProductVariant.Barcode,
                        Branch = x.Branch.Name,
                        x.Quantity,
                        x.MinQuantity,
                        CostPrice = x.ProductVariant.CostPrice,
                        Value = x.Quantity * x.ProductVariant.CostPrice
                    })
                    .OrderBy(x => x.Product)
                    .ToList();

                GridResult.DataSource = stock;

                if (GridViewResult.Columns["Quantity"] != null)
                {
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
                }
                if (GridViewResult.Columns["MinQuantity"] != null)
                {
                    GridViewResult.Columns["MinQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GridViewResult.Columns["MinQuantity"].DisplayFormat.FormatString = "0.###";
                }


                decimal totalValue = stock.Sum(x => x.Value);
                int lowStockCount = stock.Count(x => x.Quantity <= x.MinQuantity);
                LblSummary.Text = $"Total Inventory Value: {totalValue:n2}   |   Low Stock Items: {lowStockCount}";
            }
        }

        private void GridViewResult_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName != "Quantity") return;

            var row = GridViewResult.GetRow(e.RowHandle);
            var qtyProp = row.GetType().GetProperty("Quantity");
            var minProp = row.GetType().GetProperty("MinQuantity");
            if (qtyProp == null || minProp == null) return;

            decimal qty = (decimal)qtyProp.GetValue(row);
            decimal min = (decimal)minProp.GetValue(row);

            if (qty <= min)
            {
                e.Appearance.BackColor = System.Drawing.Color.MistyRose;
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UcStockReport
            // 
            this.Name = "UcStockReport";
            this.Size = new System.Drawing.Size(460, 296);
            this.ResumeLayout(false);

        }
    }
}