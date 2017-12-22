using GoodRecipe.UWP.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GoodRecipe.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeView : Page
    {
        public HomeViewModel ViewModel { get; set; }

        public HomeView()
        {
            this.InitializeComponent();
            this.ViewModel = new HomeViewModel();
            this.Loaded += HomeView_Load;
        }

        private async void HomeView_Load(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.Initialize();
        }
    }
}
