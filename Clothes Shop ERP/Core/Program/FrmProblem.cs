using Clothes_Shop_ERP.Localization;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // The one dialog every "something went wrong" path uses: plain words instead of a
    // stack trace, a reference number the customer can read out on the phone, and a
    // one-click way to get the log file to support (copy, then paste into WhatsApp).
    public class FrmProblem : XtraForm
    {
        private enum Choice { None, Continue, Retry, CloseApp }

        private const int FormWidth = 580;
        private const int TextLeft = 76;
        private const int TextWidth = FormWidth - TextLeft - 28;

        private Choice _choice = Choice.None;
        private readonly Choice _choiceOnX;
        private readonly ErrorLogEntry _entry;
        private readonly LabelControl _lblStatus;

        /// <summary>Returns true when the user chose to keep working.</summary>
        public static bool ShowCrash(ErrorLogEntry entry, bool canContinue)
        {
            string body = T("Problem_CrashBody") + (canContinue ? "\n\n" + T("Problem_CrashBodyContinue") : "");
            var buttons = canContinue
                ? new[] { Choice.CloseApp, Choice.Continue }
                : new[] { Choice.CloseApp };
            using (var form = new FrmProblem(T("Problem_CrashTitle"), body, entry, buttons, canContinue ? Choice.Continue : Choice.CloseApp))
            {
                form.ShowDialog();
                return form._choice == Choice.Continue;
            }
        }

        /// <summary>Returns true when the user wants to try connecting again.</summary>
        public static bool ShowDatabaseUnreachable(ErrorLogEntry entry)
        {
            using (var form = new FrmProblem(T("Problem_DbConnectTitle"), T("Problem_DbConnectBody"), entry,
                new[] { Choice.CloseApp, Choice.Retry }, Choice.CloseApp))
            {
                form.ShowDialog();
                return form._choice == Choice.Retry;
            }
        }

        /// <summary>A problem the user can't work around - the only way out is closing the program.</summary>
        public static void ShowBlocking(string title, string body, ErrorLogEntry entry)
        {
            using (var form = new FrmProblem(title, body, entry, new[] { Choice.CloseApp }, Choice.CloseApp))
                form.ShowDialog();
        }

        private FrmProblem(string title, string body, ErrorLogEntry entry, Choice[] actions, Choice choiceOnX)
        {
            _entry = entry;
            _choiceOnX = choiceOnX;

            // Shown before the main window exists (startup checks) it would otherwise be
            // easy to lose behind other programs.
            bool noOtherWindows = Application.OpenForms.Count == 0;

            Text = "Belnix";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = noOtherWindows;
            TopMost = noOtherWindows;
            if (LocalizationManager.CurrentLanguage != AppLanguage.English)
            {
                RightToLeft = RightToLeft.Yes;
                RightToLeftLayout = true;
            }

            var icon = new PictureBox
            {
                Image = SystemIcons.Warning.ToBitmap(),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = new Size(48, 48),
                Location = new Point(16, 20),
                BackColor = Color.Transparent
            };
            Controls.Add(icon);

            var lines = new List<LabelControl>
            {
                MakeLabel(title, new Font("Segoe UI", 13f, FontStyle.Bold)),
                MakeLabel(body, new Font("Segoe UI", 10f))
            };
            if (entry != null)
            {
                // LRE...PDF keeps "0913-101530" in order; inside Arabic text it would show as "101530-0913".
                string reference = "‪" + entry.Reference + "‬";
                lines.Add(MakeLabel(string.Format(T("Problem_ReferenceFmt"), reference), new Font("Segoe UI", 10f, FontStyle.Bold)));
                if (entry.FilePath != null)
                    lines.Add(MakeLabel(T("Problem_FileLabel") + " " + entry.FilePath, new Font("Segoe UI", 8.5f)));
            }
            lines.Add(MakeLabel(T("About_SupportText"), new Font("Segoe UI", 9.5f)));

            int y = 18;
            foreach (var label in lines)
            {
                label.Location = new Point(TextLeft, y);
                Controls.Add(label);
                y = label.Bottom + 10;
            }

            _lblStatus = MakeLabel(" ", new Font("Segoe UI", 9f, FontStyle.Bold));
            _lblStatus.Location = new Point(TextLeft, y);
            _lblStatus.Appearance.ForeColor = Color.SeaGreen;
            Controls.Add(_lblStatus);
            y = _lblStatus.Bottom + 8;

            // Action buttons sit at the far end; the file helpers at the start of the row.
            int right = FormWidth - 16;
            foreach (Choice action in actions.Reverse())
            {
                var btn = MakeButton(CaptionFor(action), action == actions.Last());
                btn.Location = new Point(right - btn.Width, y);
                Choice chosen = action;
                btn.Click += (s, e) => { _choice = chosen; Close(); };
                Controls.Add(btn);
                right = btn.Left - 8;
                if (action == actions.Last()) AcceptButton = btn;
            }

            if (entry?.FilePath != null)
            {
                var btnCopy = MakeButton(T("Problem_BtnCopyFile"), false);
                btnCopy.Location = new Point(16, y);
                btnCopy.Click += (s, e) => CopyFileToClipboard();
                Controls.Add(btnCopy);

                var btnOpen = MakeButton(T("Problem_BtnOpenFolder"), false);
                btnOpen.Location = new Point(btnCopy.Right + 8, y);
                btnOpen.Click += (s, e) => OpenFileLocation();
                Controls.Add(btnOpen);
            }

            ClientSize = new Size(FormWidth, y + 32 + 16);
            FormClosing += (s, e) => { if (_choice == Choice.None) _choice = _choiceOnX; };
        }

        private static LabelControl MakeLabel(string text, Font font)
        {
            var label = new LabelControl
            {
                Text = text,
                AutoSizeMode = LabelAutoSizeMode.None,
                Width = TextWidth
            };
            label.Appearance.Font = font;
            label.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            label.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            // Measured up front so the dialog can grow to fit however long the message is.
            Size needed = TextRenderer.MeasureText(text, font, new Size(TextWidth, 0), TextFormatFlags.WordBreak);
            label.Height = needed.Height + 4;
            return label;
        }

        private static SimpleButton MakeButton(string text, bool primary)
        {
            var font = new Font("Segoe UI", 9.5f, primary ? FontStyle.Bold : FontStyle.Regular);
            var btn = new SimpleButton { Text = text, Height = 32 };
            btn.Appearance.Font = font;
            btn.Width = Math.Max(96, TextRenderer.MeasureText(text, font).Width + 32);
            return btn;
        }

        private static string CaptionFor(Choice choice)
        {
            switch (choice)
            {
                case Choice.Continue: return T("Problem_BtnContinue");
                case Choice.Retry: return T("Problem_BtnRetry");
                default: return T("Problem_BtnCloseApp");
            }
        }

        // A file on the clipboard pastes straight into WhatsApp/Telegram/an email as an attachment.
        private void CopyFileToClipboard()
        {
            try
            {
                Clipboard.SetFileDropList(new StringCollection { _entry.FilePath });
                _lblStatus.Text = T("Problem_FileCopied");
            }
            catch
            {
                try
                {
                    Clipboard.SetText(_entry.FilePath);
                    _lblStatus.Text = T("Problem_PathCopied");
                }
                catch { }
            }
        }

        private void OpenFileLocation()
        {
            try
            {
                if (File.Exists(_entry.FilePath))
                    Process.Start("explorer.exe", "/select,\"" + _entry.FilePath + "\"");
                else
                    Process.Start("explorer.exe", "\"" + Path.GetDirectoryName(_entry.FilePath) + "\"");
            }
            catch { }
        }

        private static string T(string key) => LocalizationManager.T(key);
    }
}
