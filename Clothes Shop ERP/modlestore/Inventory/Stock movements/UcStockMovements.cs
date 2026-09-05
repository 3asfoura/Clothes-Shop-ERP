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
namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcStockMovements : DevExpress.XtraEditors.XtraUserControl
    {
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.StockMovements
                    .Include(x => x.ProductVariant).ThenInclude(v => v.Product)
                    .Include(x => x.Branch)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new
                    {
                        Product = x.ProductVariant.Product.Name + " - " + x.ProductVariant.Barcode,
                        Branch = x.Branch.Name,
                        x.MovementType,
                        x.Quantity,
                        x.RefType,
                        x.RefId,
                        x.CreatedAt
                    })
                    .ToList();
            }
            if (gridView1.Columns["Quantity"] != null)
            {
                gridView1.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["Quantity"].DisplayFormat.FormatString = "0.###";
            }
            ApplyLanguage();
        }
        public void ApplyLanguage()
        {
            if (gridView1.Columns["Product"] != null) gridView1.Columns["Product"].Caption = LocalizationManager.T("StockCount_ColProduct");
            if (gridView1.Columns["Branch"] != null) gridView1.Columns["Branch"].Caption = LocalizationManager.T("Shared_Branch");
            if (gridView1.Columns["MovementType"] != null) gridView1.Columns["MovementType"].Caption = LocalizationManager.T("StockMovements_ColMovementType");
            if (gridView1.Columns["Quantity"] != null) gridView1.Columns["Quantity"].Caption = LocalizationManager.T("StockCount_ColQuantity");
            if (gridView1.Columns["RefType"] != null) gridView1.Columns["RefType"].Caption = LocalizationManager.T("StockMovements_ColRefType");
            if (gridView1.Columns["RefId"] != null) gridView1.Columns["RefId"].Caption = LocalizationManager.T("StockMovements_ColRefId");
            if (gridView1.Columns["CreatedAt"] != null) gridView1.Columns["CreatedAt"].Caption = LocalizationManager.T("Shared_CreatedAt");
        }
        public UcStockMovements()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            Sett.CenterColumns(gridView1);
        }

        private void UcStockMovements_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
