using System.Collections.Generic;

namespace Clothes_Shop_ERP.Localization
{
    public enum AppLanguage { English, Egyptian }

    public static class LocalizationManager
    {
      
        public static AppLanguage CurrentLanguage = AppLanguage.Egyptian;

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