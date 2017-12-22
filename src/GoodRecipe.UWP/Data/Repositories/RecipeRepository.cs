using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Data.Interfaces;
using GoodRecipe.UWP.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GoodRecipe.UWP.Data.Repositories
{
    public class RecipeRepository : NotifyableClass, IRepository<Recipe>
    {
        private static readonly Lazy<RecipeRepository> _instance = new Lazy<RecipeRepository>(() => new RecipeRepository());

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();

        public ObservableCollection<Category> Categories
        {
            protected set { Set(ref _categories, value); }
            get { return _categories; }
        }

        private ObservableCollection<Recipe> _recipes = new ObservableCollection<Recipe>();

        public ObservableCollection<Recipe> Recipes
        {
            protected set { Set(ref _recipes, value); }
            get { return _recipes; }
        }

        public static RecipeRepository Instance { get { return _instance.Value; } }

        public async Task Create(Recipe recipe)
        {
            using (var context = new AppDbContext())
            {
                Recipes.Add(recipe);

                context.Recipes.Add(recipe);

                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Recipe recipe)
        {
            var todoItem = Recipes.SingleOrDefault(c => c.Id == recipe.Id);

            if (todoItem != null)
            {
                using (var context = new AppDbContext())
                {
                    Recipes.Remove(todoItem);

                    context.Recipes.Remove(todoItem);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task LoadAll()
        {
            if (Categories.Count > 0)
                return;

            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.ToListAsync();

                foreach (var category in categories)
                {
                    var recipes = await context.Recipes.
                        Where(r => r.CategoryId == category.Id)
                        .ToListAsync();

                    if (recipes != null)
                        category.Recipes = recipes;

                    Categories.Add(category);
                }
            }
        }

        public async Task Update(Recipe recipe)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(recipe).State = EntityState.Modified;

                await context.SaveChangesAsync();

                var collectionIndex = Recipes.Select((value, index) => new { value, index })
                    .Where(c => c.value.Id == recipe.Id)
                    .Select(x => x.index)
                    .First();

                Recipes[collectionIndex] = recipe;
            }
        }
    }
}
