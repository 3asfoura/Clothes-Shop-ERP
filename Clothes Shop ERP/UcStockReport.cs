using Clothes_Shop_ERP.DAL;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Clothes_Shop_ERP.Localization;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public partial class UcStockReport : DevExpress.XtraEditors.XtraUserControl
    {
        public UcStockReport()
        {
            InitializeComponent();
            RunReport();
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            btnRun.Text = LocalizationManager.T("Shared_Refresh");
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
                GridViewResult.PopulateColumns();

                if (GridViewResult.Columns["Quantity"] != null)
                {
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GridViewResult.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
                    GridViewResult.Columns["Quantity"].Caption = LocalizationManager.T("StockCount_ColQuantity");
                }
                if (GridViewResult.Columns["MinQuantity"] != null)
                {
                    GridViewResult.Columns["MinQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    GridViewResult.Columns["MinQuantity"].DisplayFormat.FormatString = "0.###";
                    GridViewResult.Columns["MinQuantity"].Caption = LocalizationManager.T("StockCount_ColMinQuantity");
                }
                if (GridViewResult.Columns["Product"] != null) GridViewResult.Columns["Product"].Caption = LocalizationManager.T("StockCount_ColProduct");
                if (GridViewResult.Columns["Barcode"] != null) GridViewResult.Columns["Barcode"].Caption = LocalizationManager.T("ProductVariants_ColBarcode");
                if (GridViewResult.Columns["Branch"] != null) GridViewResult.Columns["Branch"].Caption = LocalizationManager.T("Shared_Branch");
                if (GridViewResult.Columns["CostPrice"] != null) GridViewResult.Columns["CostPrice"].Caption = LocalizationManager.T("ProductVariants_ColCostPrice");
                if (GridViewResult.Columns["Value"] != null) GridViewResult.Columns["Value"].Caption = LocalizationManager.T("StockReport_ColValue");


                decimal totalValue = stock.Sum(x => x.Value);
                int lowStockCount = stock.Count(x => x.Quantity <= x.MinQuantity);
                LblSummary.Text = string.Format(LocalizationManager.T("StockReport_TotalInventoryValueFmt"), totalValue, lowStockCount);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunReport();
        }

        private void GridViewResult_RowCellStyle(object sender, RowCellStyleEventArgs e)
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
    }
}
