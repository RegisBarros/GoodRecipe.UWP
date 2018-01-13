using GoodRecipe.UWP.Data.Repositories;
using GoodRecipe.UWP.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GoodRecipe.UWP.ViewModels
{
    public class MyFoodViewModel
    {
        public RecipeRepository RecipeRepository { get; private set; } = RecipeRepository.Instance;

        public ObservableCollection<Category> Categories => RecipeRepository.Favorites;

        public async Task Initialize()
        {
            await RecipeRepository.LoadFavorites();
        }
    }
}
