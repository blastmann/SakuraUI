using Windows.Storage;

namespace SakuraUI.Utilities
{
    public static class RoamingSettingsHelper
    {
        private static ApplicationDataContainer AppSettings { get { return ApplicationData.Current.RoamingSettings; } }

        public static bool IsSettingsExists(string key)
        {
            return SettingsHelper.IsSettingsExists(key, AppSettings);
        }

        public static void SaveSetting<T>(string key, T value)
        {
            SettingsHelper.SaveSetting(key, value, AppSettings);
        }

        public static T LoadSetting<T>(string key)
        {
            return SettingsHelper.LoadSetting<T>(key, AppSettings);
        }

        public static T LoadSetting<T>(string key, T defaultValue)
        {
            return SettingsHelper.LoadSetting(key, defaultValue, AppSettings);
        }

        public static void Clear()
        {
            SettingsHelper.Clear(AppSettings);
        }
    }
}
