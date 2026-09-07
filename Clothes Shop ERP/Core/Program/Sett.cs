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

        // One shared, well-known folder for everything the app saves outside
        // the database itself (license file, backup configuration) - so
        // there's a single place to point a backup/recovery tool at, or to
        // find by hand if a PC needs to be rebuilt. ProgramData (not the exe's
        // own folder) because it survives a reinstall/relocation and stays
        // writable even when the app is installed under Program Files.
        public static readonly string AppDataFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Clothes Shop ERP");

        static Sett()
        {
            try { Directory.CreateDirectory(AppDataFolder); } catch { }
        }

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

        // Centers both the column headers and the cell content of every column in
        // a grid - called once per GridView so every screen in the app looks
        // consistent instead of each one centering (or not) a little differently.
        public static void CenterColumns(GridView view)
        {
            view.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            foreach (DevExpress.XtraGrid.Columns.GridColumn col in view.Columns)
                col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }

        // Shared "Export" action for every grid in the app - lets the user pick
        // Excel (for further analysis/sharing with an accountant) or PDF (for a
        // final, unchangeable printable copy) from the same Save dialog.
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
                        // Default export mode writes each cell's raw bound value, not
                        // the CustomColumnDisplayText override - so a translated grid
                        // (Status/Action/TableName/etc.) exports the original English
                        // instead of what's on screen. TextExportMode.Text makes it
                        // export what's actually displayed.
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

        // Same root cause as the export fix above: the grid's automatic tooltip
        // for a truncated cell reads the raw bound value, not the translated
        // CustomColumnDisplayText - so a cell showing Arabic flips to English the
        // moment you hover it. Routing tooltips through GetRowCellDisplayText
        // (the same call the cell's own painting already uses) keeps them in
        // sync with whatever language is actually on screen.
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
