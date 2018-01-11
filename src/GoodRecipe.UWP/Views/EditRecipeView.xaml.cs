using GoodRecipe.UWP.Models;
using GoodRecipe.UWP.ViewModels;
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
            ViewModel.Recipe = new Recipe();
        }
    }
}
