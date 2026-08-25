using Clothes_Shop_ERP.DAL;
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
        }
        public UcStockMovements()
        {
            InitializeComponent();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
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
