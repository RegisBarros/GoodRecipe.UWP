using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Services;
using Windows.UI.Xaml.Controls;

namespace GoodRecipe.UWP.ViewModels
{
    public class AppSettingsViewModel : NotifyableClass
    {
        private int? _appThemeSetting;

        public int? AppThemeSetting
        {
            get
            {
                return StorageService.GetSetting(StorageService.Settings.AppTheme, _appThemeSetting);
            }
            set
            {
                StorageService.SaveSetting(StorageService.Settings.AppTheme, value);

                ShowAppResetMessage();
            }
        }

        private bool _dialogTriggered;

        private void ShowAppResetMessage()
        {
            if (_dialogTriggered)
            {
                return;
            }

            _dialogTriggered = true;

            var dialog = new ContentDialog
            {
                Title = "Aviso",
                Content = "Você precisa reiniciar o App para trocar o estilo.",
                PrimaryButtonText = "OK"
            };

            dialog.PrimaryButtonClick += (s, e) =>
            {
                _dialogTriggered = false;
            };

            dialog.ShowAsync();
        }
    }
}
