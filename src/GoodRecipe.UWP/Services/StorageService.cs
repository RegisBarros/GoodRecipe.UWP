using Windows.Storage;

namespace GoodRecipe.UWP.Services
{
    public static class StorageService
    {
        private static ApplicationDataContainer _localSettings => ApplicationData.Current.LocalSettings;

        public enum Settings
        {
            AppTheme
        }

        public static T GetSetting<T>(Settings setting, T defaultValue)
        {
            var value = _localSettings.Values[setting.ToString()];

            if (value != null)
            {
                return (T)value;
            }
            else
            {
                return defaultValue;
            }
        }

        public static void SaveSetting(Settings setting, object value)
        {
            _localSettings.Values[setting.ToString()] = value;
        }
    }
}
