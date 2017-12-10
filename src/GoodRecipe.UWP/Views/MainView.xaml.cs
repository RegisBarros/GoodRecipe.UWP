using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoodRecipe.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private void MenuGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (NavigationPane.IsPaneOpen)
                NavigationPane.IsPaneOpen = !NavigationPane.IsPaneOpen;

            //MenuItem menu = LeftMenu.SelectedItem as MenuItem;
            //if (menu != null)
            //{
            //    if (menu.NavigateTo != null)
            //        CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { FrameContent.Navigate(menu.NavigateTo); });
            //}
        }
    }
}
