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
using DevExpress.XtraGrid.Views.Grid;

namespace Clothes_Shop_ERP
{
    public class Sett
    {
        public static SqlConnection cn = new SqlConnection(Properties.Settings.Default.cnDB);

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

    }
}
