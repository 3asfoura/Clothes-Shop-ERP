using Clothes_Shop_ERP.Localization;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    // Keeps every customer's database in step with the program - nobody runs scripts by hand.
    //
    // How it works:
    //  * The database records which updates it already has in a small DatabaseVersion table
    //    (one row per update: number, name, when, from which PC and program version).
    //  * Each schema change is a numbered script in Core\DAL\Updates (0002_AddSomething.sql, ...),
    //    compiled into Belnix.exe (see the csproj), so it can't go missing or be edited on site.
    //  * At startup every script numbered above the database's version runs, in order, each in
    //    one transaction together with its DatabaseVersion row: it applies completely or not at all.
    //
    // Rules for a new update script:
    //  * Next free 4-digit number. Never edit or renumber a script that has shipped - fix it with a new one.
    //  * Installer\Scripts\CreateDatabase.sql stays frozen as the starting schema. A brand new
    //    database gets the same updates as an old one on first start, so the two can't drift apart.
    //  * Batches may be separated by GO lines, same as in SSMS.
    //  * Nothing that can't run inside a transaction (ALTER DATABASE, BACKUP, full-text catalogs).
    public static class DatabaseUpdater
    {
        private const string ResourcePrefix = "DbUpdates.";
        private const string LockName = "Belnix_DatabaseUpdate";
        private const int ScriptTimeoutSeconds = 600;

        private static readonly Regex ScriptFileName = new Regex(@"^(\d{4})_(.+)\.sql$", RegexOptions.IgnoreCase);
        private static readonly Regex GoLine = new Regex(@"^[ \t]*GO[ \t]*(?:--[^\r\n]*)?\r?$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

        private class UpdateScript
        {
            public int Number;
            public string Name;
            public string ResourceName;
        }

        private enum Outcome { Ready, DatabaseIsNewer }

        /// <summary>
        /// Connects and brings the database up to date. Returns false when the program must
        /// not continue - the user has already been shown why.
        /// </summary>
        public static bool EnsureDatabaseReady()
        {
            SqlConnection conn;
            while (true)
            {
                conn = new SqlConnection(Properties.Settings.Default.cnDB);
                try
                {
                    conn.Open();
                    break;
                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    var entry = ErrorReporter.Log(ex, "Startup - connecting to the database");
                    if (!FrmProblem.ShowDatabaseUnreachable(entry))
                        return false;
                }
            }

            using (conn)
            {
                try
                {
                    if (ApplyPendingUpdates(conn) == Outcome.Ready)
                        return true;

                    var entry = ErrorReporter.Log(new InvalidOperationException(
                        $"Database version {ReadVersion(conn)} is newer than the newest update this build knows ({LatestKnownVersion()})."),
                        "Startup - database version check");
                    FrmProblem.ShowBlocking(T("Problem_DbNewerTitle"), T("Problem_DbNewerBody"), entry);
                    return false;
                }
                catch (Exception ex)
                {
                    var entry = ErrorReporter.Log(ex, "Startup - database update");
                    FrmProblem.ShowBlocking(T("Problem_DbUpdateTitle"), T("Problem_DbUpdateBody"), entry);
                    return false;
                }
            }
        }

        private static Outcome ApplyPendingUpdates(SqlConnection conn)
        {
            var scripts = LoadScripts();
            int latestKnown = scripts.Count == 0 ? 0 : scripts.Max(s => s.Number);

            // Cheap check first, so the normal "already up to date" start costs one query.
            int current = ReadVersion(conn);
            if (current > latestKnown) return Outcome.DatabaseIsNewer;
            if (current == latestKnown) return Outcome.Ready;

            using (ShowWorkingWindow())
            {
                // Two PCs starting the program at the same moment must not both run the same
                // update: the second one waits here, then finds there's nothing left to do.
                AcquireLock(conn);
                try
                {
                    EnsureVersionTable(conn);
                    current = ReadVersion(conn);
                    if (current > latestKnown) return Outcome.DatabaseIsNewer;

                    var pending = scripts.Where(s => s.Number > current).OrderBy(s => s.Number).ToList();
                    if (pending.Count == 0) return Outcome.Ready;

                    BackupBeforeUpdating(current);
                    foreach (var script in pending)
                        Apply(conn, script);
                }
                finally
                {
                    ReleaseLock(conn);
                }
            }
            return Outcome.Ready;
        }

        private static List<UpdateScript> LoadScripts()
        {
            var scripts = new List<UpdateScript>();
            foreach (string resource in typeof(DatabaseUpdater).Assembly.GetManifestResourceNames())
            {
                if (!resource.StartsWith(ResourcePrefix, StringComparison.Ordinal)) continue;
                var match = ScriptFileName.Match(resource.Substring(ResourcePrefix.Length));
                if (!match.Success) continue;
                scripts.Add(new UpdateScript
                {
                    Number = int.Parse(match.Groups[1].Value),
                    Name = match.Groups[2].Value,
                    ResourceName = resource
                });
            }

            // Would otherwise silently run only one of the two.
            var duplicate = scripts.GroupBy(s => s.Number).FirstOrDefault(g => g.Count() > 1);
            if (duplicate != null)
                throw new InvalidOperationException($"Two database update scripts share the number {duplicate.Key:0000}.");

            return scripts;
        }

        private static int LatestKnownVersion()
        {
            try
            {
                var scripts = LoadScripts();
                return scripts.Count == 0 ? 0 : scripts.Max(s => s.Number);
            }
            catch { return -1; }
        }

        private static int ReadVersion(SqlConnection conn)
        {
            const string sql =
                "IF OBJECT_ID(N'dbo.DatabaseVersion', N'U') IS NULL SELECT 0 " +
                "ELSE EXEC(N'SELECT ISNULL(MAX(Version), 0) FROM dbo.DatabaseVersion')";
            using (var cmd = new SqlCommand(sql, conn))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private static void EnsureVersionTable(SqlConnection conn)
        {
            const string sql = @"
IF OBJECT_ID(N'dbo.DatabaseVersion', N'U') IS NULL
CREATE TABLE dbo.DatabaseVersion (
    Version     int           NOT NULL CONSTRAINT PK_DatabaseVersion PRIMARY KEY,
    Name        nvarchar(200) NOT NULL,
    AppliedAt   datetime      NOT NULL CONSTRAINT DF_DatabaseVersion_AppliedAt DEFAULT (GETDATE()),
    AppVersion  nvarchar(50)  NULL,
    AppliedFrom nvarchar(100) NULL
);";
            using (var cmd = new SqlCommand(sql, conn))
                cmd.ExecuteNonQuery();
        }

        private static void AcquireLock(SqlConnection conn)
        {
            const string sql =
                "DECLARE @result int; " +
                "EXEC @result = sp_getapplock @Resource = @name, @LockMode = 'Exclusive', @LockOwner = 'Session', @LockTimeout = 300000; " +
                "SELECT @result;";
            using (var cmd = new SqlCommand(sql, conn) { CommandTimeout = 330 })
            {
                cmd.Parameters.AddWithValue("@name", LockName);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result < 0)
                    throw new TimeoutException($"Another PC is still updating the database (sp_getapplock returned {result}).");
            }
        }

        private static void ReleaseLock(SqlConnection conn)
        {
            try
            {
                using (var cmd = new SqlCommand("EXEC sp_releaseapplock @Resource = @name, @LockOwner = 'Session';", conn))
                {
                    cmd.Parameters.AddWithValue("@name", LockName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                // Released anyway when the connection closes.
            }
        }

        // The update itself is transactional; the backup covers an update that ran fine
        // but turns out to be wrong. Failing to back up is logged, not a reason to stop.
        private static void BackupBeforeUpdating(int fromVersion)
        {
            try
            {
                string fileName = $"{Sett.cn.Database}_before-update-v{fromVersion}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string error;

                BackupManager.LoadSettings();
                string folder = BackupManager.BackupFolder;
                if (!string.IsNullOrWhiteSpace(folder) && Directory.Exists(folder)
                    && BackupManager.BackupToFile(Path.Combine(folder, fileName), out error))
                    return;

                // A bare file name lands in SQL Server's own default backup folder, which the
                // SQL Server service can always write to (a chosen folder sometimes isn't).
                if (!BackupManager.BackupToFile(fileName, out error))
                    ErrorReporter.Log(new IOException(error), "Backup before database update (update continued without it)");
            }
            catch (Exception ex)
            {
                ErrorReporter.Log(ex, "Backup before database update (update continued without it)");
            }
        }

        private static void Apply(SqlConnection conn, UpdateScript script)
        {
            string sql;
            using (var stream = typeof(DatabaseUpdater).Assembly.GetManifestResourceStream(script.ResourceName))
            using (var reader = new StreamReader(stream))
                sql = reader.ReadToEnd();

            var batches = GoLine.Split(sql).Where(b => !string.IsNullOrWhiteSpace(b)).ToList();

            using (var transaction = conn.BeginTransaction())
            {
                int batchNumber = 0;
                try
                {
                    foreach (string batch in batches)
                    {
                        batchNumber++;
                        using (var cmd = new SqlCommand(batch, conn, transaction) { CommandTimeout = ScriptTimeoutSeconds })
                            cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(
                        "INSERT INTO dbo.DatabaseVersion (Version, Name, AppVersion, AppliedFrom) VALUES (@version, @name, @app, @pc);",
                        conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@version", script.Number);
                        cmd.Parameters.AddWithValue("@name", script.Name);
                        cmd.Parameters.AddWithValue("@app", typeof(DatabaseUpdater).Assembly.GetName().Version.ToString());
                        cmd.Parameters.AddWithValue("@pc", Environment.MachineName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Some errors already roll the transaction back on the server side.
                    try { transaction.Rollback(); } catch { }
                    throw new InvalidOperationException(
                        $"Database update {script.Number:0000}_{script.Name} failed at batch {batchNumber} of {batches.Count}. Nothing from this update was kept.", ex);
                }
            }
        }

        // The work blocks the UI thread, so without this the user sees nothing and
        // double-clicks the icon again.
        private static IDisposable ShowWorkingWindow()
        {
            var form = new XtraForm
            {
                Text = "Belnix",
                FormBorderStyle = FormBorderStyle.FixedSingle,
                ControlBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                ClientSize = new Size(440, 90),
                TopMost = true
            };
            var label = new LabelControl
            {
                Text = T("DbUpdate_Working"),
                AutoSizeMode = LabelAutoSizeMode.None,
                Dock = DockStyle.Fill
            };
            label.Appearance.Font = new Font("Segoe UI", 11f);
            label.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            label.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            label.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            form.Controls.Add(label);

            form.Show();
            Application.DoEvents();
            return form;
        }

        private static string T(string key) => LocalizationManager.T(key);
    }
}
