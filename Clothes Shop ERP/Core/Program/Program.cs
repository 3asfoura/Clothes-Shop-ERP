using Clothes_Shop_ERP.Localization;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Filtering.Internal;
using DevExpress.Utils.Svg;
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
            LocalizationManager.LoadLanguagePreference();
            if (LocalizationManager.CurrentLanguage == AppLanguage.Egyptian)
            {
                // Arabic (Egypt) culture + DevExpress Arabic localizers
                CultureInfo arabic = new CultureInfo("ar-EG");
                Thread.CurrentThread.CurrentCulture = arabic;
                Thread.CurrentThread.CurrentUICulture = arabic;

                GridLocalizer.Active = new ArabicGridLocalizer();
                FilterUIElementLocalizer.Active = new ArabicFilterLocalizer();
                Localizer.Active = new ArabicEditorsLocalizer();
            }
            else
            {
               
                CultureInfo english = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = english;
                Thread.CurrentThread.CurrentUICulture = english;
               
            }

          

            // DevExpress Localizers
          

            //////////////////////

            if (Sett.LoadDarkModePreference())
                UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.WXICompact.Darkness);

            // See LicenseManager.cs / FrmActivation.cs / FrmLicenseGenerator.cs for
            // how activation works.
            if (!LicenseManager.IsActivated())
            {
                var activation = new FrmActivation();
                activation.ShowDialog();
                if (!activation.Activated)
                    return;
            }

            Application.Run(new FrmMain());
        }
    }
}
