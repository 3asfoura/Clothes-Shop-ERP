using Clothes_Shop_ERP.Localization;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    public class ErrorLogEntry
    {
        // Short enough to read out over the phone, and it's also the timestamp the
        // entry was written under, so support can find it in the file straight away.
        public string Reference { get; set; }
        public string FilePath { get; set; }
    }

    // Last line of defence: anything no screen caught is written with full details to
    // Data\Logs and the user gets a calm "send this file to support" dialog instead of
    // the raw .NET crash box. Screens that already catch their own errors call Log() so
    // the details aren't lost once their 3-second toast disappears.
    public static class ErrorReporter
    {
        private const int KeepLogFiles = 60;
        private static readonly object FileLock = new object();
        private static int _uiThreadId;
        private static bool _cleanedUp;
        private static bool _dialogOpen;
        private static string _lastShownKey;
        private static DateTime _lastShownAt = DateTime.MinValue;

        // Must run before the first form/control is created (SetUnhandledExceptionMode requires it).
        public static void Install()
        {
            _uiThreadId = Thread.CurrentThread.ManagedThreadId;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => ReportUnhandled(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                ReportFatal(e.ExceptionObject as Exception ?? new Exception(Convert.ToString(e.ExceptionObject)), "Unhandled error (background thread)");
            // A background task nobody waited on - worth recording, not worth a dialog.
            TaskScheduler.UnobservedTaskException += (s, e) => { Log(e.Exception, "Background task"); e.SetObserved(); };
        }

        /// <summary>Writes the full details of ex to today's log file. Never throws.</summary>
        public static ErrorLogEntry Log(Exception ex, string where)
        {
            DateTime now = DateTime.Now;
            var entry = new ErrorLogEntry { Reference = now.ToString("MMdd-HHmmss") };
            try
            {
                string text = BuildEntryText(ex, where, entry.Reference, now);
                lock (FileLock)
                {
                    entry.FilePath = WriteToLogFile(text, now);
                }
            }
            catch
            {
                // Logging must never become the second error.
            }
            return entry;
        }

        /// <summary>For errors the app can't recover from: log, tell the user, and the caller exits.</summary>
        public static void ReportFatal(Exception ex, string where)
        {
            var entry = Log(ex, where);
            RunOnStaThread(() => FrmProblem.ShowCrash(entry, canContinue: false));
        }

        private static void ReportUnhandled(Exception ex)
        {
            var entry = Log(ex, "Unhandled error (screen)");

            // A screen that throws while painting would otherwise re-open this dialog on
            // every repaint, so the same error within a few seconds is logged only.
            string key = ex.GetType().FullName + "|" + ex.Message;
            if (_dialogOpen || (key == _lastShownKey && (DateTime.Now - _lastShownAt).TotalSeconds < 5))
                return;

            _dialogOpen = true;
            bool keepWorking = true;
            try
            {
                keepWorking = FrmProblem.ShowCrash(entry, canContinue: true);
            }
            finally
            {
                _dialogOpen = false;
                _lastShownKey = key;
                _lastShownAt = DateTime.Now;
            }

            // Skips FormClosing handlers on purpose - one of them could be what's broken.
            if (!keepWorking)
                Environment.Exit(1);
        }

        // The background-thread handler can't show a form on an MTA thread.
        private static void RunOnStaThread(Action action)
        {
            Action safe = () =>
            {
                try { action(); }
                catch
                {
                    try { MessageBox.Show("Belnix - an unexpected error occurred. Details were saved in the Data\\Logs folder."); } catch { }
                }
            };

            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                safe();
                return;
            }
            var thread = new Thread(() => safe());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private static string BuildEntryText(Exception ex, string where, string reference, DateTime now)
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string('=', 78));
            sb.AppendLine("Time        : " + now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("Reference   : " + reference);
            sb.AppendLine("Where       : " + where);
            sb.AppendLine("App         : " + Safe(() =>
            {
                var version = typeof(ErrorReporter).Assembly.GetName().Version;
                return $"Belnix {version} (exe built {File.GetLastWriteTime(Application.ExecutablePath):yyyy-MM-dd HH:mm})";
            }));
            sb.AppendLine("Windows     : " + Safe(() =>
                $"{Environment.OSVersion} ({(Environment.Is64BitOperatingSystem ? "64" : "32")}-bit), .NET CLR {Environment.Version}"));
            sb.AppendLine("Computer    : " + Safe(() => $"{Environment.MachineName}, Windows user '{Environment.UserName}'"));
            sb.AppendLine("Belnix user : " + Safe(() => FrmLogin.CurrentUserId == 0
                ? "(nobody logged in yet)"
                : $"#{FrmLogin.CurrentUserId} {FrmLogin.CurrentUserFullName}, branch #{FrmLogin.CurrentBranchId}"));
            sb.AppendLine("Language    : " + Safe(() => $"{LocalizationManager.CurrentLanguage} ({Thread.CurrentThread.CurrentCulture.Name})"));
            // Server and database name only - the connection string itself may hold a password.
            sb.AppendLine("Database    : " + Safe(() =>
            {
                var cs = new SqlConnectionStringBuilder(Properties.Settings.Default.cnDB);
                return $"{cs.InitialCatalog} on {cs.DataSource}";
            }));
            sb.AppendLine("Screen      : " + Safe(DescribeScreen));
            sb.AppendLine("Memory      : " + Safe(() =>
            {
                using (var p = Process.GetCurrentProcess())
                    return $"{p.WorkingSet64 / (1024 * 1024)} MB, running for {(int)(DateTime.Now - p.StartTime).TotalMinutes} min";
            }));

            sb.AppendLine();
            sb.AppendLine("--- What went wrong (outermost first) ---");
            int level = 1;
            for (Exception e = ex; e != null; e = e.InnerException, level++)
            {
                sb.AppendLine($"[{level}] {e.GetType().FullName}: {e.Message}");
                if (e is SqlException sql)
                {
                    foreach (SqlError err in sql.Errors)
                        sb.AppendLine($"     SQL error {err.Number} (severity {err.Class}, line {err.LineNumber}"
                            + (string.IsNullOrEmpty(err.Procedure) ? "" : ", in " + err.Procedure) + "): " + err.Message);
                }
            }

            sb.AppendLine();
            sb.AppendLine("--- Full technical details ---");
            sb.AppendLine(ex?.ToString() ?? "(no exception object)");
            sb.AppendLine();
            return sb.ToString();
        }

        // Which screen the user was on - only readable from the UI thread.
        private static string DescribeScreen()
        {
            if (Thread.CurrentThread.ManagedThreadId != _uiThreadId)
                return "(error was on a background thread)";

            var main = Application.OpenForms.OfType<FrmMain>().FirstOrDefault();
            string tab = main?.CurrentTabTitle;
            Form active = Form.ActiveForm;
            string window = active == null ? "none" : $"{active.GetType().Name} \"{active.Text}\"";
            return $"tab \"{tab ?? "-"}\", active window {window}";
        }

        private static string Safe(Func<string> read)
        {
            try { return read(); }
            catch (Exception ex) { return "(unavailable: " + ex.GetType().Name + ")"; }
        }

        private static string WriteToLogFile(string text, DateTime now)
        {
            string fileName = $"Belnix-Errors-{now:yyyy-MM-dd}.txt";
            // Same Data folder as Sett.AppDataFolder, resolved here without touching Sett:
            // if Sett itself failed to initialise, that is exactly the error to record.
            string[] folders =
            {
                Path.Combine(Application.StartupPath, "Data", "Logs"),
                Path.Combine(Path.GetTempPath(), "Belnix", "Logs")
            };

            foreach (string folder in folders)
            {
                try
                {
                    Directory.CreateDirectory(folder);
                    string path = Path.Combine(folder, fileName);
                    // UTF-8 with BOM so Notepad shows Arabic names and messages correctly.
                    File.AppendAllText(path, text, Encoding.UTF8);
                    CleanupOldLogs(folder);
                    return path;
                }
                catch
                {
                    // Not writable - try the next folder.
                }
            }
            return null;
        }

        private static void CleanupOldLogs(string folder)
        {
            if (_cleanedUp) return;
            _cleanedUp = true;
            try
            {
                foreach (var old in new DirectoryInfo(folder).GetFiles("Belnix-Errors-*.txt")
                             .OrderByDescending(f => f.Name)
                             .Skip(KeepLogFiles))
                    old.Delete();
            }
            catch { }
        }
    }
}
