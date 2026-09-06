using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.BarCode;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public class BarcodeLabelData
    {
        public string ProductName { get; set; }
        public string VariantInfo { get; set; }   // e.g. "Red - L", blank if not applicable
        public string Barcode { get; set; }
        public decimal Price { get; set; }
    }

    // Prints one or more copies of a small product label (name, price, scannable
    // barcode) on the same 58mm thermal roll the receipts use - stacked one
    // after another like a sticker roll, since that's how small shops usually
    // print labels for new stock. Uses DevExpress's BarCodeControl just to
    // render the barcode image; everything else is drawn the same way as
    // ReceiptPrinter (plain GDI+ PrintDocument).
    public static class BarcodeLabelPrinter
    {
        private const int PaperWidthHundredthsInch = 228;
        private const int MarginHundredthsInch = 6;
        private const int MinPaperHeightHundredthsInch = 150;
        private const float LabelHeight = 150;

        public static void Print(BarcodeLabelData data, int quantity)
        {
            try
            {
                using (var doc = BuildDocument(data, quantity))
                {
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Error"), ex.Message);
            }
        }

        public static void Preview(BarcodeLabelData data, int quantity)
        {
            using (var doc = BuildDocument(data, quantity))
            using (var preview = new PrintPreviewDialog { Document = doc, Width = 500, Height = 700 })
            {
                preview.ShowDialog();
            }
        }

        private static PrintDocument BuildDocument(BarcodeLabelData data, int quantity)
        {
            float contentWidth = PaperWidthHundredthsInch - 2 * MarginHundredthsInch;
            int paperHeight = Math.Max((int)(quantity * LabelHeight) + 2 * MarginHundredthsInch + 20, MinPaperHeightHundredthsInch);

            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Label", PaperWidthHundredthsInch, paperHeight);
            doc.DefaultPageSettings.Margins = new Margins(MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch);
            doc.PrintPage += (s, e) => DrawLabels(e.Graphics, data, quantity, contentWidth);
            return doc;
        }

        private static void DrawLabels(Graphics g, BarcodeLabelData data, int quantity, float width)
        {
            var nameFont = new Font("Segoe UI", 9, FontStyle.Bold);
            var infoFont = new Font("Segoe UI", 8);
            var priceFont = new Font("Segoe UI", 10, FontStyle.Bold);
            var brush = Brushes.Black;
            var centerFormat = new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap };

            float barcodeHeight = 55;
            float y = 0;

            using (var barcodeControl = new BarCodeControl())
            {
                barcodeControl.Symbology = new Code128Generator();
                barcodeControl.ShowText = true;
                barcodeControl.AutoModule = true;   // shrink the bar width to fit instead of refusing to draw
                barcodeControl.Text = data.Barcode;
                barcodeControl.Size = new Size((int)width, (int)barcodeHeight);
                barcodeControl.CreateControl();

                using (var barcodeBmp = new Bitmap(barcodeControl.Width, barcodeControl.Height))
                {
                    barcodeControl.DrawToBitmap(barcodeBmp, new Rectangle(Point.Empty, barcodeControl.Size));

                    for (int i = 0; i < quantity; i++)
                    {
                        float labelTop = y;

                        g.DrawString(data.ProductName, nameFont, brush, new RectangleF(0, y, width, 16), centerFormat);
                        y += 16;

                        if (!string.IsNullOrWhiteSpace(data.VariantInfo))
                        {
                            g.DrawString(data.VariantInfo, infoFont, brush, new RectangleF(0, y, width, 13), centerFormat);
                            y += 13;
                        }

                        g.DrawString(data.Price.ToString("n2"), priceFont, brush, new RectangleF(0, y, width, 17), centerFormat);
                        y += 19;

                        g.DrawImage(barcodeBmp, 0, y);
                        y += barcodeHeight;

                        // Perforation-style cut line between copies (not after the last one).
                        if (i < quantity - 1)
                        {
                            y += 4;
                            using (var pen = new Pen(Color.Gray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                                g.DrawLine(pen, 0, y, width, y);
                            y += 6;
                        }
                    }
                }
            }
        }
    }
}
