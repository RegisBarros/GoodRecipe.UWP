using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Data.Repositories;
using GoodRecipe.UWP.Models;
using System.Collections.ObjectModel;

namespace GoodRecipe.UWP.ViewModels
{
    public class EditRecipeViewModel : NotifyableClass
    {
        public CategoryRepository CategoryRepository { get; private set; } = CategoryRepository.Instance;

        public ObservableCollection<Category> Categories => CategoryRepository.Categories;

        private Recipe _recipe;

        public Recipe Recipe
        {
            get { return _recipe; }
            set { Set(ref _recipe, value); }
        }
    }
}
