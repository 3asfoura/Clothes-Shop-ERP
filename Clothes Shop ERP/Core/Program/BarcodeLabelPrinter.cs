using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.BarCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
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

    // Prints product labels (name, price, barcode) on the same 58mm thermal roll as receipts.
    public static class BarcodeLabelPrinter
    {
        private const int PaperWidthHundredthsInch = 228;
        private const int MarginHundredthsInch = 6;
        private const int MinPaperHeightHundredthsInch = 150;

        public static void Print(BarcodeLabelData data, int quantity)
        {
            PrintBatch(new[] { (data, quantity) });
        }

        public static void Preview(BarcodeLabelData data, int quantity)
        {
            using (var doc = BuildDocument(new List<(BarcodeLabelData Data, int Quantity)> { (data, quantity) }))
            using (var preview = new PrintPreviewDialog { Document = doc, Width = 500, Height = 700 })
            {
                preview.ShowDialog();
            }
        }

        // Prints every (label, quantity) pair as ONE continuous print job instead of
        // calling Print() once per item - sending one job per variant meant that on a
        // machine whose default printer is a "Print to PDF"-style virtual printer, the
        // "Save Print Output As" dialog popped up separately for every single item
        // (confirmed in the field: printing 20 variants meant closing 20 save dialogs).
        // One combined job asks (or actually prints) exactly once for the whole batch,
        // real printer or virtual.
        public static void PrintBatch(IEnumerable<(BarcodeLabelData Data, int Quantity)> items)
        {
            try
            {
                using (var doc = BuildDocument(items.ToList()))
                {
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
                ErrorReporter.Log(ex, "Printing barcode labels");
                Sett.MsgRed(LocalizationManager.T("Shared_Error"), ex.Message);
            }
        }

        // Actual drawn height of one label: 16 name + 13 variant + 19 price
        // + 55 barcode + 10 separator. Must stay in step with DrawLabel below.
        private const float LabelBlockHeight = 113;

        // Upper bound only - the real limit is whatever the printer says fits, see
        // the PrintPage handler. Keeps the requested paper size sane for a roll printer.
        private const int MaxLabelsPerPage = 20;

        // Every printer has an unprintable strip at the edge of the page that
        // e.MarginBounds doesn't account for, so a label that "fits" by that measure
        // can still come out with its bottom sliced off - confirmed in the field: the
        // last label on a page printed its bars but lost the number underneath.
        // Held back from the bottom of every page so nothing lands in that strip.
        private const float BottomSafetyPad = 30;

        private const float BarcodeHeight = 55;

        private static PrintDocument BuildDocument(List<(BarcodeLabelData Data, int Quantity)> items)
        {
            float contentWidth = PaperWidthHundredthsInch - 2 * MarginHundredthsInch;

            // Flattened into one flat list of individual labels so pagination is just an
            // index into it. Deliberately a list + index rather than a queue that gets
            // consumed: a PrintDocument can legitimately be rendered more than once
            // (the preview dialog's own Print button re-runs it), and a drained queue
            // would make that second run come out blank. BeginPrint resets the index.
            var labels = new List<BarcodeLabelData>();
            foreach (var item in items)
                for (int i = 0; i < item.Quantity; i++)
                    labels.Add(item.Data);

            // Every distinct barcode's bitmap is rendered once, up front, before the
            // print job itself starts - building a fresh BarCodeControl per label
            // *during* PrintPage (i.e. while the print engine has an active GDI
            // context) turned out to be unreliable for a bulk batch: some labels came
            // out with a solid black block instead of the actual bars, confirmed in
            // the field on a real batch, though not on a smaller one. Generating them
            // all beforehand, outside that context, removes the shared cause.
            var barcodeBitmaps = new Dictionary<string, Bitmap>();
            foreach (var barcode in labels.Select(d => d.Barcode).Distinct())
                barcodeBitmaps[barcode] = RenderBarcodeBitmap(barcode, (int)contentWidth, (int)BarcodeHeight);

            int labelsOnThisPage = Math.Min(Math.Max(labels.Count, 1), MaxLabelsPerPage);
            // The slack here covers BottomSafetyPad too, so a roll printer that DOES
            // honour this size still fits the full MaxLabelsPerPage per page.
            int paperHeight = Math.Max(
                (int)(labelsOnThisPage * LabelBlockHeight) + 2 * MarginHundredthsInch + (int)BottomSafetyPad + 30,
                MinPaperHeightHundredthsInch);

            var doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Label", PaperWidthHundredthsInch, paperHeight);
            doc.DefaultPageSettings.Margins = new Margins(MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch, MarginHundredthsInch);
            // Draw relative to the margin box rather than the raw page corner, so the
            // fit check below and the drawing itself use the same coordinate space.
            doc.OriginAtMargins = true;

            int nextLabel = 0;
            doc.BeginPrint += (s, e) => nextLabel = 0;
            doc.PrintPage += (s, e) =>
            {
                // The custom PaperSize requested above is only a REQUEST. A driver that
                // doesn't support it (Microsoft Print to PDF, most office printers)
                // silently substitutes its own default page, so the number of labels
                // that actually fit is whatever e.MarginBounds reports - not what was
                // asked for. Assuming the requested size instead is what made labels
                // past the first ~10 get drawn off the page and vanish: confirmed in
                // the field, a 120-label batch lost roughly half of them, with the one
                // straddling the page edge coming out as a black stub.
                float usableHeight = e.MarginBounds.Height - BottomSafetyPad;
                float labelWidth = Math.Min(contentWidth, e.MarginBounds.Width);

                float y = 0;
                bool drewAny = false;
                // The "|| !drewAny" guarantees at least one label per page: without it,
                // a page too short for even a single label would draw nothing, advance
                // nothing, and set HasMorePages forever - an endless print job.
                while (nextLabel < labels.Count && (!drewAny || y + LabelBlockHeight <= usableHeight))
                {
                    var data = labels[nextLabel];
                    y = DrawLabel(e.Graphics, data, barcodeBitmaps[data.Barcode], labelWidth, y);
                    nextLabel++;
                    drewAny = true;
                }
                e.HasMorePages = nextLabel < labels.Count;
            };
            // Disposed with the document, not at EndPrint - the bitmaps have to survive
            // a second render of the same document (preview dialog's Print button).
            doc.Disposed += (s, e) =>
            {
                foreach (var bmp in barcodeBitmaps.Values) bmp.Dispose();
            };
            return doc;
        }

        // Rendered well above screen resolution, then scaled down into the label's
        // barcode rectangle when drawn - bars stay crisp enough for a scanner instead
        // of being a blurry 96-DPI screen capture stretched onto paper.
        private const int BarcodeRenderDpi = 300;

        private static Bitmap RenderBarcodeBitmap(string barcodeText, int width, int height)
        {
            using (var barcodeControl = new BarCodeControl())
            {
                barcodeControl.Symbology = new Code128Generator();
                barcodeControl.ShowText = true;
                barcodeControl.AutoModule = true;   // shrink the bar width to fit instead of refusing to draw
                barcodeControl.Text = barcodeText;
                barcodeControl.Size = new Size(width, height);

                // Forced, never inherited: the control would otherwise pick up whatever
                // DevExpress skin the app is currently running (Belnix has a dark mode),
                // and a dark skin renders the barcode light-on-dark - which prints as a
                // solid black block AND is unreadable to every barcode scanner, since
                // scanners require dark bars on a light background. Confirmed in the
                // field: labels printed as black rectangles while the app was in dark mode.
                barcodeControl.BackColor = Color.White;
                barcodeControl.ForeColor = Color.Black;

                // ExportToImage is DevExpress's own documented rasteriser for this control
                // (and takes a DPI). Control.DrawToBitmap, used before, instead captures
                // whatever the control's on-screen painting produced - theme colours
                // included - and is unreliable for a control that was never actually shown.
                using (var image = barcodeControl.ExportToImage(System.Drawing.Imaging.ImageFormat.Png, BarcodeRenderDpi))
                {
                    return new Bitmap(image);
                }
            }
        }

        private static float DrawLabel(Graphics g, BarcodeLabelData data, Bitmap barcodeBmp, float width, float startY)
        {
            var nameFont = new Font("Segoe UI", 9, FontStyle.Bold);
            var infoFont = new Font("Segoe UI", 8);
            var priceFont = new Font("Segoe UI", 10, FontStyle.Bold);
            var brush = Brushes.Black;
            var centerFormat = new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap };

            float y = startY;

            g.DrawString(data.ProductName, nameFont, brush, new RectangleF(0, y, width, 16), centerFormat);
            y += 16;

            if (!string.IsNullOrWhiteSpace(data.VariantInfo))
            {
                g.DrawString(data.VariantInfo, infoFont, brush, new RectangleF(0, y, width, 13), centerFormat);
                y += 13;
            }

            g.DrawString(data.Price.ToString("n2"), priceFont, brush, new RectangleF(0, y, width, 17), centerFormat);
            y += 19;

            // Explicit destination rectangle, NOT DrawImage(bmp, x, y): the bitmap is
            // rendered at 300 DPI for sharpness, so drawing it "at its natural size"
            // scales it by its own pixel count and it comes out several inches wide,
            // swallowing the whole page (confirmed in the field). The rectangle pins it
            // to the label's own area no matter what resolution it was rendered at.
            g.DrawImage(barcodeBmp, new RectangleF(0, y, width, BarcodeHeight));
            y += BarcodeHeight;

            // Perforation-style cut line after every label, including the very last
            // one overall - a batch draws several different labels back to back, and
            // there's no easy way to know here whether more will follow this one. A
            // trailing dashed line before the final cut is harmless.
            y += 4;
            using (var pen = new Pen(Color.Gray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                g.DrawLine(pen, 0, y, width, y);
            y += 6;

            return y;
        }
    }
}
