using GoodRecipe.UWP.Models;
using GoodRecipe.UWP.ViewModels;
using Newtonsoft.Json;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoodRecipe.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditRecipeView : Page
    {
        public EditRecipeViewModel ViewModel { get; } = new EditRecipeViewModel();

        public EditRecipeView()
        {
            this.InitializeComponent();

            ViewModel.Initialize();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null && e.Parameter is Recipe)
            {
                var recipe = (Recipe)e.Parameter;

                ViewModel.Recipe = recipe;
            }
            else
            {
                ViewModel.Recipe = new Recipe();
            }
        }

        public void CancelRecipeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Frame.Navigate(typeof(HomeView)); });
        }
    }
}
