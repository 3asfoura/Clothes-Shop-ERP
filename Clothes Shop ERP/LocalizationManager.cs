using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Clothes_Shop_ERP.Localization
{
    public enum AppLanguage { English, Egyptian }

    public static class LocalizationManager
    {
      
        public static AppLanguage CurrentLanguage = AppLanguage.Egyptian;
        private static readonly string SettingsFilePath =
            Path.Combine(Application.StartupPath, "lang.settings");
        public static void LoadLanguagePreference()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string saved = File.ReadAllText(SettingsFilePath).Trim();
                    if (Enum.TryParse(saved, out AppLanguage lang))
                    {
                        CurrentLanguage = lang;
                    }
                }
            }
            catch
            {
               
            }
        }
        public static void SaveLanguagePreference()
        {
            try
            {
                File.WriteAllText(SettingsFilePath, CurrentLanguage.ToString());
            }
            catch
            {
               
            }
        }
        public static string T(string key)
        {
            Dictionary<string, string> dict = CurrentLanguage == AppLanguage.English
                ? Lang_English.Strings
                : Lang_Egyptian.Strings;

            if (dict.TryGetValue(key, out string value))
                return value;

            return key;
        }
    }
}