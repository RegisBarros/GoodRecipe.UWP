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

        private ObservableCollection<Category> GetCategories()
        {
            var categories = new ObservableCollection<Category>();
            categories.Add(new Category("Bolos e Tortas", string.Empty, GetRecipes()));
            categories.Add(new Category("Lanches", string.Empty, GetRecipes()));
            categories.Add(new Category("Saladas e Molhos", string.Empty, GetRecipes()));

            return categories;
        }

        private ObservableCollection<Recipe> GetRecipes()
        {
            var recipes = new ObservableCollection<Recipe>();
            recipes.Add(new Recipe("Minha receita 1", "Minha descrição de receita 1", 10));
            recipes.Add(new Recipe("Minha receita 2", "Minha descrição de receita 2", 11));
            recipes.Add(new Recipe("Minha receita 3", "Minha descrição de receita 3", 12));

            return recipes;
        }
    }
}
