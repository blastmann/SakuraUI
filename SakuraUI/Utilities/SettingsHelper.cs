using System;
using Windows.Storage;

namespace SakuraUI.Utilities
{
    public class SettingsHelper
    {
        public static bool IsSettingsExists(string key, ApplicationDataContainer container)
        {
            if (container == null) throw new ArgumentNullException("container", "Container can't be null");

            lock (container)
            {
                return container.Values.ContainsKey(key);
            }
        }

        public static void SaveSetting<T>(string key, T value, ApplicationDataContainer container)
        {
            if (container == null) throw new ArgumentNullException("container", "Container can't be null");

            lock (container)
            {
                container.Values[key] = value;
            }
        }

        public static T LoadSetting<T>(string key, ApplicationDataContainer container)
        {
            if (!IsSettingsExists(key, container)) return default(T);

            lock (container)
            {
                return (T)container.Values[key];
            }
        }

        public static T LoadSetting<T>(string key, T defaultValue, ApplicationDataContainer container)
        {
            if (container == null) throw new ArgumentNullException("container", "Container can't be null");
            return IsSettingsExists(key, container) ? LoadSetting<T>(key, container) : defaultValue;
        }

        public static void Clear(ApplicationDataContainer container)
        {
            if (container == null) throw new ArgumentNullException("container", "Container can't be null");
            container.Values.Clear();
        }
    }
}
