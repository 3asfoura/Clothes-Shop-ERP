using Clothes_Shop_ERP.Localization;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    // Simple, static info screen - built entirely in code, no Designer/LayoutControl.
    public partial class UcAbout : DevExpress.XtraEditors.XtraUserControl
    {
        private LabelControl lblActivationValue;
        private LabelControl lblExpiryValue;
        private TextEdit txtMachineId;

        // This screen draws its own fixed light/dark colors instead of relying on the skin.
        private bool _isDark;

        public UcAbout()
        {
            InitializeComponent();
            _isDark = Sett.LoadDarkModePreference();
            BuildUi();
            RefreshActivationStatus();

            // Refreshes colors live if the user toggles dark mode while this tab stays open.
            Sett.DarkModeChanged += OnDarkModeChanged;
            this.Disposed += (s, e) => Sett.DarkModeChanged -= OnDarkModeChanged;
        }

        private void OnDarkModeChanged()
        {
            if (this.IsDisposed) return;
            _isDark = Sett.LoadDarkModePreference();

            var oldControls = this.Controls.Cast<Control>().ToArray();
            this.Controls.Clear();
            foreach (var c in oldControls) c.Dispose();

            BuildUi();
            RefreshActivationStatus();
        }

        private void BuildUi()
        {
            Color contentBack = _isDark ? Color.FromArgb(32, 32, 32) : Color.FromArgb(240, 240, 240);

            var pnlHeader = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = 110,
                BorderStyle = BorderStyles.NoBorder
            };
            LockAppearance(pnlHeader);
            // Same fixed color as the login screen's Login button.
            pnlHeader.Appearance.BackColor = Color.FromArgb(13, 59, 120);
            pnlHeader.Appearance.Options.UseBackColor = true;

            var lblAppName = new LabelControl
            {
                Text = "Belnix",
                Location = new Point(24, 20),
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.White
            };
            var lblTagline = new LabelControl
            {
                Text = LocalizationManager.T("About_Tagline"),
                Location = new Point(26, 66),
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(190, 215, 245)
            };
            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(lblTagline);

            // Padded content area holding stacked "cards", Dashboard-style.
            var pnlContent = new PanelControl
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BorderStyle = BorderStyles.NoBorder
            };
            LockAppearance(pnlContent);
            pnlContent.Appearance.BackColor = contentBack;
            pnlContent.Appearance.Options.UseBackColor = true;

            // Dock stacking claims space in reverse add order - last added is docked first.
            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlHeader);

            var cardSupport = MakeCard(160);
            BuildSupportCard(cardSupport);
            pnlContent.Controls.Add(cardSupport);

            var spacer1 = new Panel { Dock = DockStyle.Top, Height = 16, BackColor = Color.Transparent };
            pnlContent.Controls.Add(spacer1);

            var cardLicense = MakeCard(310);
            BuildLicenseCard(cardLicense);
            pnlContent.Controls.Add(cardLicense);
        }

        private PanelControl MakeCard(int height)
        {
            var card = new PanelControl
            {
                Dock = DockStyle.Top,
                Height = height,
                BorderStyle = BorderStyles.Simple,
                Padding = new Padding(20)
            };
            LockAppearance(card);
            card.Appearance.BackColor = _isDark ? Color.FromArgb(48, 48, 48) : Color.White;
            card.Appearance.Options.UseBackColor = true;
            card.Appearance.BorderColor = _isDark ? Color.FromArgb(70, 70, 70) : Color.Silver;
            card.Appearance.Options.UseBorderColor = true;
            return card;
        }

        // Stops the skin from repainting this panel out of sync with its explicit-color labels.
        private static void LockAppearance(PanelControl control)
        {
            control.LookAndFeel.UseDefaultLookAndFeel = false;
            control.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
        }

        private void BuildLicenseCard(PanelControl card)
        {
            const int x = 20;
            int y = 18;
            Color mainText = _isDark ? Color.WhiteSmoke : Color.Black;
            Color hintText = _isDark ? Color.Silver : Color.Gray;

            var lblCardTitle = new LabelControl { Text = LocalizationManager.T("About_LicenseInfo"), Location = new Point(x, y), Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = mainText };
            card.Controls.Add(lblCardTitle);
            y += 36;

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var lblVersion = new LabelControl
            {
                Text = LocalizationManager.T("About_Version") + ":  " + version.ToString(),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 10),
                ForeColor = mainText
            };
            card.Controls.Add(lblVersion);
            y += 32;

            lblActivationValue = new LabelControl { Location = new Point(x, y), Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            card.Controls.Add(lblActivationValue);
            y += 26;

            lblExpiryValue = new LabelControl { Location = new Point(x, y), Font = new Font("Segoe UI", 9), ForeColor = hintText };
            card.Controls.Add(lblExpiryValue);
            y += 42;

            var lblMachineIdTitle = new LabelControl { Text = LocalizationManager.T("About_MachineId"), Location = new Point(x, y), Font = new Font("Segoe UI", 9), ForeColor = hintText };
            card.Controls.Add(lblMachineIdTitle);
            y += 22;

            txtMachineId = new TextEdit { Text = LicenseManager.GetMachineId(), Location = new Point(x, y), Width = 340 };
            txtMachineId.Properties.ReadOnly = true;
            card.Controls.Add(txtMachineId);

            var btnCopyId = new SimpleButton { Text = LocalizationManager.T("About_BtnCopy"), Location = new Point(x + 350, y), Width = 90 };
            btnCopyId.Click += (s, e) =>
            {
                Clipboard.SetText(txtMachineId.Text);
                Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Activation_IdCopied"));
            };
            card.Controls.Add(btnCopyId);
        }

        private void BuildSupportCard(PanelControl card)
        {
            const int x = 20;
            int y = 18;
            Color mainText = _isDark ? Color.WhiteSmoke : Color.Black;

            var lblCardTitle = new LabelControl { Text = LocalizationManager.T("About_Support"), Location = new Point(x, y), Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = mainText };
            card.Controls.Add(lblCardTitle);
            y += 36;

            var lblSupportValue = new LabelControl { Text = LocalizationManager.T("About_SupportText"), Location = new Point(x, y), Font = new Font("Segoe UI", 10), ForeColor = mainText };
            card.Controls.Add(lblSupportValue);
            y += 40;

            var btnWhatsApp = MakeIconButton(
                MakeCircleIcon(Color.FromArgb(37, 211, 102), DrawWhatsAppGlyph),
                "WhatsApp",
                () => { try { Process.Start("https://wa.me/201128259064"); } catch { } });
            btnWhatsApp.Location = new Point(x, y);
            card.Controls.Add(btnWhatsApp);

            // No page link yet - wire up once there's a URL.
            var btnFacebook = MakeIconButton(
                MakeCircleIcon(Color.FromArgb(24, 119, 242), DrawFacebookGlyph),
                "Facebook",
                () => { });
            btnFacebook.Location = new Point(x + 56, y);
            card.Controls.Add(btnFacebook);
        }

        // Small brand-colored circular icon buttons, drawn in code.
        private static Bitmap MakeCircleIcon(Color bgColor, Action<Graphics, Rectangle> drawGlyph, int size = 40)
        {
            var bmp = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(bgColor))
                    g.FillEllipse(brush, 0, 0, size - 1, size - 1);
                drawGlyph(g, new Rectangle(0, 0, size, size));
            }
            return bmp;
        }

        private static PictureBox MakeIconButton(Bitmap icon, string toolTipText, Action onClick)
        {
            var pic = new PictureBox
            {
                Image = icon,
                Size = new Size(icon.Width, icon.Height),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Cursor = Cursors.Hand
            };
            var tip = new ToolTip();
            tip.SetToolTip(pic, toolTipText);
            pic.Click += (s, e) => onClick();
            // PictureBox doesn't dispose its Image, so do it ourselves to avoid a leak on rebuild.
            pic.Disposed += (s, e) => icon.Dispose();
            return pic;
        }

        private static void DrawWhatsAppGlyph(Graphics g, Rectangle bounds)
        {
            using (var brush = new SolidBrush(Color.White))
            {
                var body = new Rectangle(bounds.X + 9, bounds.Y + 9, bounds.Width - 18, bounds.Height - 20);
                using (var path = RoundedRect(body, 5))
                    g.FillPath(brush, path);

                Point[] tail =
                {
                    new Point(body.Left + 8, body.Bottom - 1),
                    new Point(body.Left + 8, body.Bottom + 6),
                    new Point(body.Left + 15, body.Bottom - 1)
                };
                g.FillPolygon(brush, tail);
            }
        }

        private static void DrawFacebookGlyph(Graphics g, Rectangle bounds)
        {
            using (var font = new Font("Georgia", 20, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("f", font, brush, bounds, sf);
            }
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void RefreshActivationStatus()
        {
            DateTime? expiry;
            bool activated = LicenseManager.IsActivated(out expiry);

            lblActivationValue.Text = LocalizationManager.T("About_ActivationStatus") + ":  "
                + (activated ? LocalizationManager.T("About_Activated") : LocalizationManager.T("About_NotActivated"));
            lblActivationValue.ForeColor = activated ? Color.SeaGreen : Color.Crimson;

            lblExpiryValue.Text = expiry.HasValue
                ? string.Format(LocalizationManager.T("About_ExpiresOnFmt"), expiry.Value)
                : LocalizationManager.T("About_NoExpiry");
        }
    }
}
