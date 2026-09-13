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

        // Printers have an unprintable strip at the page edge that e.MarginBounds does
        // not account for, so a line that "fits" by that measure can still be sliced in
        // half. Held back from the bottom of every page.
        private const float BottomSafetyPad = 30;

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
                ErrorReporter.Log(ex, "Printing receipt");
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

        // One drawable piece of the receipt (a line, a divider, a centred heading...)
        // with its own height, so a receipt can be split across pages at a sane
        // boundary instead of being drawn as one indivisible block.
        private class ReceiptBlock
        {
            public float Height;
            public Action<Graphics, float> Draw;   // (graphics, y position to draw at)
        }

        private static PrintDocument BuildDocument(ReceiptData data)
        {
            float contentWidth = PaperWidthHundredthsInch - 2 * MarginHundredthsInch;

            // Dry-run against a throwaway bitmap to measure text (positions are worked
            // out once here and captured, so printing later just replays them).
            List<ReceiptBlock> blocks;
            using (var bmp = new Bitmap(1, 1))
            using (var measureGraphics = Graphics.FromImage(bmp))
            {
                measureGraphics.PageUnit = GraphicsUnit.Display; // hundredths of an inch, matching PaperSize
                blocks = BuildReceiptBlocks(measureGraphics, data, contentWidth);
            }

            float contentHeight = blocks.Sum(b => b.Height);
            // Slack covers BottomSafetyPad, so a roll printer that DOES honour this size
            // still prints the whole receipt as a single continuous page.
            int paperHeight = Math.Max(
                (int)Math.Ceiling(contentHeight) + 2 * MarginHundredthsInch + (int)BottomSafetyPad + 30,
                MinPaperHeightHundredthsInch);

            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", PaperWidthHundredthsInch, paperHeight);
            doc.DefaultPageSettings.Margins = new Margins(MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch);
            doc.OriginAtMargins = true;

            // Index rather than a consumed collection: the preview dialog's own Print
            // button renders the same document a second time, which would otherwise
            // come out blank. BeginPrint restarts it for every render.
            int nextBlock = 0;
            doc.BeginPrint += (s, e) => nextBlock = 0;
            doc.PrintPage += (s, e) =>
            {
                // The tall custom PaperSize above is only a REQUEST. A 58mm roll printer
                // honours it, but Microsoft Print to PDF and ordinary office printers
                // silently substitute A4/Letter - and the receipt then simply ran off the
                // bottom of the page and was lost, taking the subtotal, discount and TOTAL
                // with it. e.MarginBounds is what the printer actually granted, so that's
                // what decides where this page ends and the next begins.
                float usableHeight = e.MarginBounds.Height - BottomSafetyPad;
                float y = 0;
                bool drewAny = false;
                while (nextBlock < blocks.Count && (!drewAny || y + blocks[nextBlock].Height <= usableHeight))
                {
                    blocks[nextBlock].Draw(e.Graphics, y);
                    y += blocks[nextBlock].Height;
                    nextBlock++;
                    drewAny = true;
                }
                e.HasMorePages = nextBlock < blocks.Count;
            };
            return doc;
        }

        /// <summary>Lays the receipt out into page-splittable blocks. Text is measured
        /// (and every x position worked out) once here against <paramref name="g"/>;
        /// each block's captured Draw action just replays that at whatever y it lands on.</summary>
        private static List<ReceiptBlock> BuildReceiptBlocks(Graphics g, ReceiptData data, float width)
        {
            bool isRtl = LocalizationManager.CurrentLanguage == Clothes_Shop_ERP.Localization.AppLanguage.Egyptian;

            var shopFont = new Font("Consolas", 13, FontStyle.Bold);
            var smallFont = new Font("Consolas", 8f);
            var normalFont = new Font("Consolas", 9);
            var boldFont = new Font("Consolas", 9, FontStyle.Bold);
            var totalFont = new Font("Consolas", 12, FontStyle.Bold);
            var brush = Brushes.Black;

            var blocks = new List<ReceiptBlock>();
            const float lineHeight = 17;
            const float smallLineHeight = 13;

            // Only text that actually contains Arabic gets RTL shaping - forcing it on pure Latin/numeric breaks it.
            bool ContainsArabic(string s) => !string.IsNullOrEmpty(s) && s.Any(c => c >= '؀' && c <= 'ۿ');

            // Position (x) and text shaping (RTL flag) are kept independent - mixing them misplaced text.
            void DrawAt(Graphics gr, string text, Font font, float x, float w, float y)
            {
                var fmt = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.NoWrap | (ContainsArabic(text) ? StringFormatFlags.DirectionRightToLeft : 0)
                };
                gr.DrawString(text, font, brush, new RectangleF(x, y, w + 4, lineHeight + 6), fmt);
            }

            void Center(string text, Font font, float height = lineHeight)
            {
                if (string.IsNullOrWhiteSpace(text)) return;
                float w = g.MeasureString(text, font).Width;
                float x = Math.Max((width - w) / 2, 0);
                blocks.Add(new ReceiptBlock { Height = height, Draw = (gr, y) => DrawAt(gr, text, font, x, w, y) });
            }
            // Draws at the start of the reading direction - right edge for Arabic, left edge for English.
            void Line(string text, Font font)
            {
                float w = g.MeasureString(text, font).Width;
                float x = isRtl ? Math.Max(width - w, 0) : 0;
                blocks.Add(new ReceiptBlock { Height = lineHeight, Draw = (gr, y) => DrawAt(gr, text, font, x, w, y) });
            }
            // The classic two-column receipt row: label at the reading start, number at
            // the reading end (right/left for Arabic, left/right for English).
            void TwoCol(string label, string number, Font font)
            {
                float labelW = g.MeasureString(label, font).Width;
                float numberW = g.MeasureString(number, font).Width;
                float labelX = isRtl ? Math.Max(width - labelW, 0) : 0;
                float numberX = isRtl ? 0 : Math.Max(width - numberW, 0);
                blocks.Add(new ReceiptBlock
                {
                    Height = lineHeight,
                    Draw = (gr, y) =>
                    {
                        DrawAt(gr, label, font, labelX, labelW, y);
                        DrawAt(gr, number, font, numberX, numberW, y);
                    }
                });
            }
            // Label and value are drawn as two independently-shaped pieces, not concatenated (broke mixed-script names).
            void LabelValue(string label, string value, Font font)
            {
                float labelW = g.MeasureString(label, font).Width;
                float valueW = g.MeasureString(value, font).Width;
                const float gap = 4;
                float labelX, valueX;
                if (isRtl)
                {
                    labelX = Math.Max(width - labelW, 0);
                    valueX = Math.Max(labelX - gap - valueW, 0);
                }
                else
                {
                    labelX = 0;
                    valueX = labelW + gap;
                }
                blocks.Add(new ReceiptBlock
                {
                    Height = lineHeight,
                    Draw = (gr, y) =>
                    {
                        DrawAt(gr, label, font, labelX, labelW, y);
                        DrawAt(gr, value, font, valueX, valueW, y);
                    }
                });
            }
            void Divider(float thickness = 1)
            {
                blocks.Add(new ReceiptBlock
                {
                    Height = 2 + thickness + 4,
                    Draw = (gr, y) =>
                    {
                        using (var pen = new Pen(Color.Black, thickness))
                            gr.DrawLine(pen, 0, y + 2, width, y + 2);
                    }
                });
            }
            void Space(float height)
            {
                blocks.Add(new ReceiptBlock { Height = height, Draw = (gr, y) => { } });
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
            Space(4);
            TwoCol(LocalizationManager.T("Receipt_SubtotalLabel"), data.SubTotal.ToString("n2"), normalFont);
            if (data.Discount > 0)
                TwoCol(LocalizationManager.T("POS_Discount"), "-" + data.Discount.ToString("n2"), normalFont);
            Divider();
            TwoCol(LocalizationManager.T("Receipt_TotalLabel"), data.NetTotal.ToString("n2"), totalFont);
            Divider(2);

            Space(6);
            Center(LocalizationManager.T("Receipt_ThankYou"), normalFont);

            return blocks;
        }
    }
}
