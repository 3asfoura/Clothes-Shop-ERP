using Clothes_Shop_ERP.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
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
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
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

    // Prints a thermal-style receipt via the default Windows printer (PrintDocument).
    public static class ReceiptPrinter
    {
        // 58mm thermal paper, in hundredths of an inch.
        private const int PaperWidthHundredthsInch = 228;
        private const int MarginHundredthsInch = 6;
        // Height is calculated per receipt from its actual line count, not a fixed page size.
        private const int MinPaperHeightHundredthsInch = 300;

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
                // A printing failure shouldn't block the sale itself.
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
            float contentWidth = PaperWidthHundredthsInch - 2 * MarginHundredthsInch;

            // Dry-run against a throwaway bitmap just to measure this receipt's height.
            float contentHeight;
            using (var bmp = new Bitmap(1, 1))
            using (var measureGraphics = Graphics.FromImage(bmp))
            {
                measureGraphics.PageUnit = GraphicsUnit.Display; // hundredths of an inch, matching PaperSize
                contentHeight = DrawReceipt(measureGraphics, data, contentWidth);
            }

            int paperHeight = Math.Max((int)Math.Ceiling(contentHeight) + 2 * MarginHundredthsInch + 20, MinPaperHeightHundredthsInch);

            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", PaperWidthHundredthsInch, paperHeight);
            doc.DefaultPageSettings.Margins = new Margins(MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch);
            doc.PrintPage += (s, e) => DrawReceipt(e.Graphics, data, contentWidth);
            return doc;
        }

        /// <summary>Draws the receipt and returns the total content height (same units as the Graphics' PageUnit).</summary>
        private static float DrawReceipt(Graphics g, ReceiptData data, float width)
        {
            bool isRtl = LocalizationManager.CurrentLanguage == Clothes_Shop_ERP.Localization.AppLanguage.Egyptian;

            var shopFont = new Font("Consolas", 13, FontStyle.Bold);
            var smallFont = new Font("Consolas", 8f);
            var normalFont = new Font("Consolas", 9);
            var boldFont = new Font("Consolas", 9, FontStyle.Bold);
            var totalFont = new Font("Consolas", 12, FontStyle.Bold);
            var brush = Brushes.Black;

            float y = 0;
            const float lineHeight = 17;
            const float smallLineHeight = 13;

            // Only text that actually contains Arabic gets RTL shaping - forcing it on pure Latin/numeric breaks it.
            bool ContainsArabic(string s) => !string.IsNullOrEmpty(s) && s.Any(c => c >= '؀' && c <= 'ۿ');

            // Position (x) and text shaping (RTL flag) are kept independent - mixing them misplaced text.
            void DrawAt(string text, Font font, float x, float w)
            {
                var fmt = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.NoWrap | (ContainsArabic(text) ? StringFormatFlags.DirectionRightToLeft : 0)
                };
                g.DrawString(text, font, brush, new RectangleF(x, y, w + 4, lineHeight + 6), fmt);
            }

            void Center(string text, Font font, float height = lineHeight)
            {
                if (string.IsNullOrWhiteSpace(text)) return;
                float w = g.MeasureString(text, font).Width;
                DrawAt(text, font, Math.Max((width - w) / 2, 0), w);
                y += height;
            }
            // Draws at the start of the reading direction - right edge for Arabic, left edge for English.
            void Line(string text, Font font)
            {
                float w = g.MeasureString(text, font).Width;
                float x = isRtl ? Math.Max(width - w, 0) : 0;
                DrawAt(text, font, x, w);
                y += lineHeight;
            }
            // The classic two-column receipt row: label at the reading start, number at
            // the reading end (right/left for Arabic, left/right for English).
            void TwoCol(string label, string number, Font font)
            {
                float labelW = g.MeasureString(label, font).Width;
                float numberW = g.MeasureString(number, font).Width;
                float labelX = isRtl ? Math.Max(width - labelW, 0) : 0;
                float numberX = isRtl ? 0 : Math.Max(width - numberW, 0);
                DrawAt(label, font, labelX, labelW);
                DrawAt(number, font, numberX, numberW);
                y += lineHeight;
            }
            // Label and value are drawn as two independently-shaped pieces, not concatenated (broke mixed-script names).
            void LabelValue(string label, string value, Font font)
            {
                float labelW = g.MeasureString(label, font).Width;
                float valueW = g.MeasureString(value, font).Width;
                const float gap = 4;
                if (isRtl)
                {
                    float labelX = Math.Max(width - labelW, 0);
                    float valueX = Math.Max(labelX - gap - valueW, 0);
                    DrawAt(label, font, labelX, labelW);
                    DrawAt(value, font, valueX, valueW);
                }
                else
                {
                    DrawAt(label, font, 0, labelW);
                    DrawAt(value, font, labelW + gap, valueW);
                }
                y += lineHeight;
            }
            void Divider(float thickness = 1)
            {
                y += 2;
                using (var pen = new Pen(Color.Black, thickness))
                    g.DrawLine(pen, 0, y, width, y);
                y += thickness + 4;
            }

            // Header: shop name + optional address/phone, all centered.
            Center(data.ShopName, shopFont);
            Center(data.ShopAddress, smallFont, smallLineHeight);
            Center(data.ShopPhone, smallFont, smallLineHeight);
            Divider(2);

            // Invoice number / date, cashier, customer, payment method.
            Line(LocalizationManager.T("Receipt_InvoiceLabel") + " " + data.InvoiceNumber, smallFont);
            Line(data.Date.ToString("dd/MM/yyyy HH:mm"), smallFont);
            if (!string.IsNullOrWhiteSpace(data.Cashier))
                LabelValue(LocalizationManager.T("Receipt_CashierLabel"), data.Cashier, normalFont);
            if (!string.IsNullOrWhiteSpace(data.Customer))
                LabelValue(LocalizationManager.T("POS_Customer"), data.Customer, normalFont);
            LabelValue(LocalizationManager.T("POS_PaymentMethod"), data.PaymentMethod, normalFont);
            Divider();

            // Line items: product name on its own line, quantity/price under it,
            // with the line total right-aligned like a real receipt.
            foreach (var line in data.Lines)
            {
                Line(line.Product, boldFont);
                TwoCol($"   {line.Quantity:0.##} x {line.UnitPrice:n2}", line.LineTotal.ToString("n2"), normalFont);
            }
            Divider();

            decimal totalQty = data.Lines.Sum(l => l.Quantity);
            TwoCol(LocalizationManager.T("Receipt_ItemsLabel"), $"{data.Lines.Count} ({totalQty:0.##} {LocalizationManager.T("Receipt_UnitsLabel")})", smallFont);
            y += 4;
            TwoCol(LocalizationManager.T("Receipt_SubtotalLabel"), data.SubTotal.ToString("n2"), normalFont);
            if (data.Discount > 0)
                TwoCol(LocalizationManager.T("POS_Discount"), "-" + data.Discount.ToString("n2"), normalFont);
            Divider();
            TwoCol(LocalizationManager.T("Receipt_TotalLabel"), data.NetTotal.ToString("n2"), totalFont);
            Divider(2);

            y += 6;
            Center(LocalizationManager.T("Receipt_ThankYou"), normalFont);

            return y;
        }
    }
}
