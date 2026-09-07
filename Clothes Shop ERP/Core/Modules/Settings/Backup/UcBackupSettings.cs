using Clothes_Shop_ERP.Localization;
using System;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.modlestore
{
    public partial class UcBackupSettings : DevExpress.XtraEditors.XtraUserControl
    {
        public UcBackupSettings()
        {
            InitializeComponent();
            BackupManager.LoadSettings();
            TxtFolder.Text = BackupManager.BackupFolder;
            ApplyLanguage();
            RefreshLastBackupLabel();
        }

        public void ApplyLanguage()
        {
            lblFolder.Text = LocalizationManager.T("Backup_Folder");
            btnBrowse.Text = LocalizationManager.T("Backup_BtnBrowse");
            btnSave.Text = LocalizationManager.T("Backup_BtnSave");
            btnBackupNow.Text = LocalizationManager.T("Backup_BtnBackupNow");
            btnSaveAs.Text = LocalizationManager.T("Backup_BtnSaveAs");
            lblHint.Text = LocalizationManager.T("Backup_Hint");
            RefreshLastBackupLabel();
        }

        private void RefreshLastBackupLabel()
        {
            LblLastBackup.Text = BackupManager.LastBackupAt.HasValue
                ? string.Format(LocalizationManager.T("Backup_LastBackupFmt"), BackupManager.LastBackupAt.Value)
                : LocalizationManager.T("Backup_NeverBackedUp");
        }


        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanEdit("BackupSettings"))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Shared_NoPermissionMsg"));
                return;
            }

            using (var dialog = new SaveFileDialog
            {
                Filter = LocalizationManager.T("Backup_FileFilter"),
                FileName = BackupManager.SuggestedFileName()
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                Cursor.Current = Cursors.WaitCursor;
                bool ok = BackupManager.BackupToFile(dialog.FileName, out string error);
                Cursor.Current = Cursors.Default;

                if (ok)
                    Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Backup_Success"));
                else
                    Sett.MsgRed(LocalizationManager.T("Shared_Error"), LocalizationManager.T("Backup_Failed") + "\n\n" + error);
            }
        }

        private void btnBackupNow_Click(object sender, EventArgs e)
        {
            if (!PermissionManager.CanEdit("BackupSettings"))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Shared_NoPermissionMsg"));
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtFolder.Text))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Backup_FolderRequired"));
                return;
            }

            BackupManager.BackupFolder = TxtFolder.Text;
            BackupManager.SaveSettings();

            Cursor.Current = Cursors.WaitCursor;
            bool ok = BackupManager.RunBackupNow(out string error);
            Cursor.Current = Cursors.Default;

            RefreshLastBackupLabel();

            if (ok)
                Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Backup_Success"));
            else
                Sett.MsgRed(LocalizationManager.T("Shared_Error"), LocalizationManager.T("Backup_Failed") + "\n\n" + error);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!PermissionManager.CanEdit("BackupSettings"))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Shared_NoPermissionMsg"));
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtFolder.Text))
            {
                Sett.MsgRed(LocalizationManager.T("Shared_Warning"), LocalizationManager.T("Backup_FolderRequired"));
                return;
            }

            BackupManager.BackupFolder = TxtFolder.Text;
            BackupManager.SaveSettings();
            Sett.MsgGreen(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Backup_FolderSaved"));
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (!string.IsNullOrWhiteSpace(TxtFolder.Text))
                    dialog.SelectedPath = TxtFolder.Text;

                if (dialog.ShowDialog() == DialogResult.OK)
                    TxtFolder.Text = dialog.SelectedPath;
            }
        }

        // Manual, one-off copy: the user picks the exact file and folder right now
        // (a USB drive, Desktop, anywhere) via the normal Windows save dialog -
        // independent of the configured backup folder and the daily schedule above.

    }
}
