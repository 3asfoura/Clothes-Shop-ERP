using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Once a day, takes a native SQL Server backup (.bak) into the configured folder, keeping the last 14.
    public static class BackupManager
    {
        private const int KeepBackups = 14;
        private static readonly string SettingsFilePath = ResolveSettingsFilePath();

        // Migrates an old exe-folder backup.settings into Sett.AppDataFolder.
        private static string ResolveSettingsFilePath()
        {
            string newPath = Path.Combine(Sett.AppDataFolder, "backup.settings");
            string oldPath = Path.Combine(Application.StartupPath, "backup.settings");
            try
            {
                if (!File.Exists(newPath) && File.Exists(oldPath))
                    File.Copy(oldPath, newPath);
            }
            catch { }
            return newPath;
        }

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
                // Not worth interrupting the user for - next backup will just re-save it.
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

            string dbName;
            string fullPath;
            try
            {
                Directory.CreateDirectory(BackupFolder);
                dbName = Sett.cn.Database;
                string fileName = $"{dbName}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                fullPath = Path.Combine(BackupFolder, fileName);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

            if (!BackupToFile(fullPath, out error)) return false;

            LastBackupAt = DateTime.Now;
            SaveSettings();
            CleanupOldBackups(dbName);
            return true;
        }

        /// <summary>Suggests a default file name for a manual "Save Database As..." dialog.</summary>
        public static string SuggestedFileName() => $"{Sett.cn.Database}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

        /// <summary>One-off manual backup to a user-picked path (e.g. a USB drive).</summary>
        public static bool BackupToFile(string fullPath, out string error)
        {
            error = null;
            try
            {
                string dbName = Sett.cn.Database;

                // Dedicated connection so a backup can never collide with normal screen activity.
                using (var conn = new SqlConnection(Sett.cn.ConnectionString))
                {
                    conn.Open();
                    string escapedPath = fullPath.Replace("'", "''"); // BACKUP DATABASE can't take a parameterized path.
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
                // Common cause: the SQL Server service account can't write to the chosen folder.
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
