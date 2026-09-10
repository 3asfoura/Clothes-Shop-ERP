using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Windows.Forms;
using static System.Console;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using LocalizationManager = Clothes_Shop_ERP.Localization.LocalizationManager;

namespace Clothes_Shop_ERP
{
    public class Sett
    {
        public static SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnDB);

        // Shared "Data" folder next to the exe for everything saved outside the database.
        public static readonly string AppDataFolder = ResolveAppDataFolder();

        // Migrates license.dat/backup.settings/lang.settings from their old locations.
        private static string ResolveAppDataFolder()
        {
            string newFolder = Path.Combine(Application.StartupPath, "Data");
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string[] oldFolders =
            {
                Path.Combine(programData, "Belnix"),
                Path.Combine(programData, "Clothes Shop ERP")
            };
            try
            {
                Directory.CreateDirectory(newFolder);
                bool alreadyMigrated = File.Exists(Path.Combine(newFolder, "license.dat"))
                                     || File.Exists(Path.Combine(newFolder, "backup.settings"));
                if (!alreadyMigrated)
                {
                    foreach (string oldFolder in oldFolders)
                    {
                        if (!Directory.Exists(oldFolder)) continue;
                        foreach (string file in Directory.GetFiles(oldFolder))
                        {
                            string dest = Path.Combine(newFolder, Path.GetFileName(file));
                            if (!File.Exists(dest)) File.Copy(file, dest, true);
                        }
                    }
                }

                string oldLangFile = Path.Combine(Application.StartupPath, "lang.settings");
                string newLangFile = Path.Combine(newFolder, "lang.settings");
                if (!File.Exists(newLangFile) && File.Exists(oldLangFile))
                    File.Copy(oldLangFile, newLangFile, true);
            }
            catch { }
            return newFolder;
        }

        static Sett()
        {
            try { Directory.CreateDirectory(AppDataFolder); } catch { }
        }

        private static readonly string DarkModeSettingsPath = Path.Combine(AppDataFolder, "darkmode.settings");

        public static bool LoadDarkModePreference()
        {
            try
            {
                return File.Exists(DarkModeSettingsPath) && File.ReadAllText(DarkModeSettingsPath).Trim() == "Dark";
            }
            catch
            {
                return false;
            }
        }

        public static void SaveDarkModePreference(bool isDark)
        {
            try { File.WriteAllText(DarkModeSettingsPath, isDark ? "Dark" : "Light"); } catch { }
            DarkModeChanged?.Invoke();
        }

        // Lets a screen with its own fixed colors (see UcAbout) refresh live when dark mode toggles.
        public static event Action DarkModeChanged;

        // Message Aler - Icon Code
        // Star = "\uf005" ,Bell = "\uf0f3" , refrwsh = "\uf021"
        // ,angles-left = "\uf100" , album-circle-plus
        public static void MsgAlert(string msg, eDesktopAlertColor clr = eDesktopAlertColor.Default, int Duration_Sec = 2, String MsgIcon = "\uf0f3")  // Star = "\uf005" ,Bell = "\uf0f3" , refrwsh = "\uf021" ,angles-left = "\uf100" , album-circle-plus
        {

            /* 
              // Example 
             //Sett.MsgAlert("Text", eDesktopAlertColor.Blue, 5);
            */
            DesktopAlert.Show(msg, MsgIcon, eSymbolSet.Awesome, Color.Empty, clr, eAlertPosition.BottomLeft, Duration_Sec, 0, null);
        }
     
        public static void MsgRed(string title,string description)
        {
            Sett.MsgAlert(title + "\n" + description, eDesktopAlertColor.Red, 3);
        }
        public static void MsgBlue(string title, string description)
        {
            Sett.MsgAlert(title + "\n" + description, eDesktopAlertColor.Blue, 3);
        }
        public static void MsgGreen(string title, string description)
        {
            Sett.MsgAlert(title + "\n" + description, eDesktopAlertColor.Green, 3);
        }

        // Centers headers and cell content - called once per GridView for a consistent look everywhere.
        public static void CenterColumns(GridView view)
        {
            view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        // Shared "Export" action for every grid in the app - Excel or PDF from one Save dialog.
        public static void ExportGrid(GridControl grid, string suggestedFileName)
        {
            using (var dlg = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx|PDF Document (*.pdf)|*.pdf",
                FileName = suggestedFileName
            })
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    if (dlg.FilterIndex == 1)
                    {
                        // TextExportMode.Text exports the translated display text, not the raw bound value.
                        var xlsxOptions = new DevExpress.XtraPrinting.XlsxExportOptions
                        {
                            TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text
                        };
                        grid.ExportToXlsx(dlg.FileName, xlsxOptions);
                    }
                    else
                        grid.ExportToPdf(dlg.FileName);

                    MsgBlue(LocalizationManager.T("Shared_Success"), LocalizationManager.T("Export_Done"));
                }
                catch (Exception ex)
                {
                    MsgRed(LocalizationManager.T("Shared_Error"), ex.Message);
                }
            }
        }

        // Same issue as the export fix - routes tooltips through the same translated display text.
        public static void FixCellTooltips(GridView view)
        {
            var controller = new ToolTipController();
            controller.GetActiveObjectInfo += (s, e) =>
            {
                if (e.Info != null) return;
                var hitInfo = view.CalcHitInfo(e.ControlMousePosition);
                if (!hitInfo.InRowCell || hitInfo.Column == null) return;

                string text = view.GetRowCellDisplayText(hitInfo.RowHandle, hitInfo.Column);
                e.Info = new ToolTipControlInfo(new GridCellId(hitInfo.RowHandle, hitInfo.Column), text);
            };
            view.GridControl.ToolTipController = controller;
        }

        private struct GridCellId : IEquatable<GridCellId>
        {
            private readonly int _rowHandle;
            private readonly DevExpress.XtraGrid.Columns.GridColumn _column;
            public GridCellId(int rowHandle, DevExpress.XtraGrid.Columns.GridColumn column)
            {
                _rowHandle = rowHandle;
                _column = column;
            }
            public bool Equals(GridCellId other) => _rowHandle == other._rowHandle && _column == other._column;
            public override bool Equals(object obj) => obj is GridCellId other && Equals(other);
            public override int GetHashCode() => _rowHandle.GetHashCode() ^ (_column?.GetHashCode() ?? 0);
        }

    }
}
