using Clothes_Shop_ERP.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public class ReceiptLine
    {
        public string Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class ReceiptData
    {
        public string ShopName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Cashier { get; set; }
        public string PaymentMethod { get; set; }
        public List<ReceiptLine> Lines { get; set; } = new List<ReceiptLine>();
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal NetTotal { get; set; }
    }

    // Draws and prints a simple thermal-style receipt (narrow width, monospace
    // font) for a completed sale. Uses the normal Windows printing API
    // (PrintDocument), so it works with any thermal printer that has a
    // Windows driver installed - which is how the large majority of USB/network
    // receipt printers are set up. Sends to whatever the default printer is;
    // if that's not the receipt printer, change the Windows default printer
    // for this PC, or use Preview() below to check output without printing.
    public static class ReceiptPrinter
    {
        // 80mm thermal paper, in hundredths of an inch (matches PrintDocument's units).
        private const int PaperWidthHundredthsInch = 315;
        private const int PaperHeightHundredthsInch = 1100;

        public static void Print(ReceiptData data)
        {
            try
            {
                using (var doc = BuildDocument(data))
                {
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
                // A printing failure shouldn't block the sale that already went
                // through - just tell the cashier so they can print manually/retry.
                Sett.MsgRed(LocalizationManager.T("Shared_Error"), ex.Message);
            }
        }

        /// <summary>Shows a print preview instead of printing - useful for testing the layout, or reprinting an old invoice.</summary>
        public static void Preview(ReceiptData data)
        {
            using (var doc = BuildDocument(data))
            using (var preview = new PrintPreviewDialog { Document = doc, Width = 500, Height = 700 })
            {
                preview.ShowDialog();
            }
        }

        private static PrintDocument BuildDocument(ReceiptData data)
        {
            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", PaperWidthHundredthsInch, PaperHeightHundredthsInch);
            doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            doc.PrintPage += (s, e) => DrawReceipt(e.Graphics, data);
            return doc;
        }

        private static void DrawReceipt(Graphics g, ReceiptData data)
        {
            var titleFont = new Font("Consolas", 11, FontStyle.Bold);
            var normalFont = new Font("Consolas", 8.5f);
            var boldFont = new Font("Consolas", 9, FontStyle.Bold);
            var brush = Brushes.Black;

            float width = g.VisibleClipBounds.Width;
            float y = 0;
            const float lineHeight = 16;

            void Center(string text, Font font)
            {
                float x = (width - g.MeasureString(text, font).Width) / 2;
                g.DrawString(text, font, brush, Math.Max(x, 0), y);
                y += lineHeight;
            }
            void Line(string text, Font font)
            {
                g.DrawString(text, font, brush, 0, y);
                y += lineHeight;
            }
            void Divider()
            {
                g.DrawLine(Pens.Black, 0, y + 2, width, y + 2);
                y += lineHeight;
            }

            Center(data.ShopName, titleFont);
            Center(data.InvoiceNumber, normalFont);
            Center(data.Date.ToString("dd/MM/yyyy HH:mm"), normalFont);
            Divider();

            if (!string.IsNullOrWhiteSpace(data.Customer))
                Line(LocalizationManager.T("POS_Customer") + " " + data.Customer, normalFont);
            Line(LocalizationManager.T("POS_PaymentMethod") + " " + data.PaymentMethod, normalFont);
            if (!string.IsNullOrWhiteSpace(data.Cashier))
                Line(data.Cashier, normalFont);
            Divider();

            foreach (var line in data.Lines)
            {
                Line(line.Product, normalFont);
                Line($"  {line.Quantity:0.##} x {line.UnitPrice:n2} = {line.LineTotal:n2}", normalFont);
            }
            Divider();

            Line($"{LocalizationManager.T("Shared_TotalAmount")}: {data.SubTotal:n2}", normalFont);
            if (data.Discount > 0)
                Line($"{LocalizationManager.T("POS_Discount")} {data.Discount:n2}", normalFont);
            Line($"{LocalizationManager.T("Shared_ColTotal")}: {data.NetTotal:n2}", boldFont);
            y += 6;
            Center(LocalizationManager.T("Receipt_ThankYou"), normalFont);
        }
    }
}
