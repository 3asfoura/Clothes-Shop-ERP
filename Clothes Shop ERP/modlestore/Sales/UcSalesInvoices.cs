using Clothes_Shop_ERP.DAL;
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
            GetData();
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsCustomization.AllowSort = false;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        public void GetData()
        {
            using (var db = new ClothesShopDBContext())
            {
                gridView1.GridControl.DataSource = db.SalesInvoices
                    .Include(x => x.Customer)
                    .Include(x => x.Branch)
                    .OrderByDescending(x => x.InvoiceDate)
                    .Select(x => new
                    {
                        x.Id,
                        x.InvoiceNumber,
                        Customer = x.Customer != null ? x.Customer.Name : "Walk-in",
                        Branch = x.Branch.Name,
                        x.InvoiceDate,
                        x.NetAmount,
                        x.PaidAmount,
                        x.Status
                    })
                    .ToList();
            }
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
                menu.Items.Add("ViewDetails", null, (s, ev) => ViewDetails());
          
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

                XtraMessageBox.Show(message, "Invoice Details");
            }
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Row) return;
            if (e.HitInfo.InRow) gridView1.FocusedRowHandle = e.HitInfo.RowHandle;

            e.Menu.Items.Clear();
            if (e.HitInfo.InRow)
            {
                e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("View Details", (s, ev) => ViewDetails()));
            }
        }
    }
}
