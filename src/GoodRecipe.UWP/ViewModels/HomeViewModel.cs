using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Data.Repositories;
using GoodRecipe.UWP.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GoodRecipe.UWP.ViewModels
{
    public class HomeViewModel : NotifyableClass
    {
        public RecipeRepository RecipeRepository { get; private set; } = RecipeRepository.Instance;

        public ObservableCollection<Category> Categories => RecipeRepository.Categories;

        public async Task Initialize()
        {
            await RecipeRepository.LoadAll();
        }

        
    }
}
