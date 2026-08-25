using DevExpress.Utils.Filtering.Internal;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clothes_Shop_ERP
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ////////////////////////////
            // Arabic Culture
            CultureInfo arabic = new CultureInfo("ar-EG");

            Thread.CurrentThread.CurrentCulture = arabic;
            Thread.CurrentThread.CurrentUICulture = arabic;

            // DevExpress Localizers
            GridLocalizer.Active = new ArabicGridLocalizer();
            FilterUIElementLocalizer.Active = new ArabicFilterLocalizer();
            Localizer.Active = new ArabicEditorsLocalizer();

            //////////////////////

            Application.Run(new FrmMain());
        }
    }
}
