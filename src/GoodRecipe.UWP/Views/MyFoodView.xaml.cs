using GoodRecipe.UWP.Models;
using GoodRecipe.UWP.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoodRecipe.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyFoodView : Page
    {
        public MyFoodViewModel ViewModel { get; set; }

        public MyFoodView()
        {
            this.InitializeComponent();
            this.ViewModel = new MyFoodViewModel();
            this.Loaded += MyFoodView_Load;
        }

        private async void MyFoodView_Load(object sender, RoutedEventArgs e)
        {
            await ViewModel.Initialize();
        }

        private void StackPanel_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var recipe = ((FrameworkElement)e.OriginalSource).DataContext as Recipe;

            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Frame.Navigate(typeof(EditRecipeView), recipe); });
        }
    }
}
