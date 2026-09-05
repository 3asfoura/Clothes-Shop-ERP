using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Automatic database backup. Once a day (the first time the app runs that
    // day) it takes a native SQL Server backup (.bak) into a folder the user
    // picks under Settings, and keeps only the most recent copies so the disk
    // doesn't fill up. Settings live in a small local text file, the same way
    // LocalizationManager stores the chosen language - no database table needed.
    public static class BackupManager
    {
        private const int KeepBackups = 14;
        private static readonly string SettingsFilePath =
            Path.Combine(Application.StartupPath, "backup.settings");

        public static string BackupFolder { get; set; }
        public static DateTime? LastBackupAt { get; set; }

        public static void LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsFilePath)) return;
                string[] lines = File.ReadAllLines(SettingsFilePath);
                BackupFolder = lines.Length > 0 ? lines[0] : null;
                if (lines.Length > 1 && DateTime.TryParse(lines[1], out DateTime last))
                    LastBackupAt = last;
            }
            catch
            {
                // Missing/corrupt settings file just means "not configured yet".
            }
        }

        public static void SaveSettings()
        {
            try
            {
                File.WriteAllLines(SettingsFilePath, new[]
                {
                    BackupFolder ?? "",
                    LastBackupAt?.ToString("o") ?? ""
                });
            }
            catch
            {
                // If we can't write the settings file, the next manual backup
                // will just re-save it - not worth interrupting the user for.
            }
        }

        /// <summary>Runs a backup only if one hasn't already run today. Returns true if a backup actually ran.</summary>
        public static bool RunBackupIfDue()
        {
            LoadSettings();
            if (string.IsNullOrWhiteSpace(BackupFolder) || !Directory.Exists(BackupFolder)) return false;
            if (LastBackupAt.HasValue && LastBackupAt.Value.Date == DateTime.Today) return false;
            return RunBackupNow(out _);
        }

        /// <summary>Runs a backup right now, regardless of when the last one ran. On failure, error explains why (shown to the user).</summary>
        public static bool RunBackupNow(out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(BackupFolder))
            {
                error = "No backup folder is set.";
                return false;
            }

            try
            {
                Directory.CreateDirectory(BackupFolder);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

            string dbName = Sett.cn.Database;
            string fileName = $"{dbName}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            string fullPath = Path.Combine(BackupFolder, fileName);

            if (!BackupToFile(fullPath, out error)) return false;

            LastBackupAt = DateTime.Now;
            SaveSettings();
            CleanupOldBackups(dbName);
            return true;
        }

        /// <summary>Suggests a default file name for a manual "Save Database As..." dialog.</summary>
        public static string SuggestedFileName() => $"{Sett.cn.Database}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

        /// <summary>
        /// Backs up straight to whatever exact path the user picked themselves (e.g. via a
        /// SaveFileDialog onto a USB drive) - a one-off manual copy, independent of the
        /// configured BackupFolder and the daily-automatic bookkeeping above.
        /// </summary>
        public static bool BackupToFile(string fullPath, out string error)
        {
            error = null;
            try
            {
                string dbName = Sett.cn.Database;

                // A dedicated connection, separate from the shared Sett.cn used
                // everywhere else, so a backup can never collide with normal
                // screen activity on the same connection.
                using (var conn = new SqlConnection(Sett.cn.ConnectionString))
                {
                    conn.Open();
                    // BACKUP DATABASE doesn't support a parameterized file path in all
                    // SQL Server driver versions, so the path is escaped and inlined instead.
                    string escapedPath = fullPath.Replace("'", "''");
                    using (var cmd = new SqlCommand($"BACKUP DATABASE [{dbName}] TO DISK = '{escapedPath}'", conn))
                    {
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Most common real-world cause: BACKUP DATABASE runs inside the SQL
                // Server process itself, under its own Windows service account - not
                // the account running this app. If the chosen folder is somewhere
                // like Desktop/Documents under a user profile, that service account
                // usually can't write there, even though this app can. Point the
                // backup folder at something like C:\ClothesShopBackups instead, and
                // grant that folder full control to the SQL Server service account
                // (or to Everyone, for a quick local test).
                error = ex.Message;
                return false;
            }
        }

        private static void CleanupOldBackups(string dbName)
        {
            try
            {
                var files = new DirectoryInfo(BackupFolder)
                    .GetFiles($"{dbName}_*.bak")
                    .OrderByDescending(f => f.CreationTime)
                    .Skip(KeepBackups);

                foreach (var f in files)
                    f.Delete();
            }
            catch
            {
                // Cleanup is a nice-to-have; a failure here shouldn't affect the backup itself.
            }
        }
    }
}
