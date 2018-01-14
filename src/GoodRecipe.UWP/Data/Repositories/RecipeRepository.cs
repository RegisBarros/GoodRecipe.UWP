using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Data.Interfaces;
using GoodRecipe.UWP.Models;
using GoodRecipe.UWP.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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


        private ObservableCollection<Category> _favorites = new ObservableCollection<Category>();

        public ObservableCollection<Category> Favorites
        {
            protected set { Set(ref _favorites, value); }
            get { return _favorites; }
        }

        public static RecipeRepository Instance { get { return _instance.Value; } }

        public async Task Create(Recipe recipe)
        {
            using (var context = new AppDbContext())
            {
                context.Recipes.Add(recipe);

                if (recipe.Category != null)
                {
                    context.Entry(recipe.Category).State = EntityState.Unchanged;
                }

                await context.SaveChangesAsync();
            }

            // Save picture in app folder
            await MediaService.SavePicture(recipe.Id, recipe.Picture);
            recipe.ImageSource = await MediaService.GetPicture(recipe.Id, recipe.Picture);

            Category category = Categories.FirstOrDefault(c => c.Id == recipe.CategoryId);
            category.Recipes.Add(recipe);
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
            await Seed();

            if (Categories.Any())
                return;

            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.ToListAsync();

                foreach (var category in categories)
                {
                    var recipes = await context.Recipes.
                        Where(r => r.CategoryId == category.Id)
                        .ToListAsync();

                    if (recipes != null && recipes.Any())
                    {
                        category.Recipes = recipes;

                        Categories.Add(category);
                    }

                }
            }

            foreach (var category in Categories)
            {
                foreach (var recipe in category.Recipes)
                {
                    recipe.ImageSource = await MediaService.GetPicture(recipe.Id, recipe.Picture);
                }
            }
        }

        public async Task Update(Recipe recipe)
        {
            using (var context = new AppDbContext())
            {
                context.Entry(recipe).State = EntityState.Modified;

                await context.SaveChangesAsync();

                // Save picture in app folder
                await MediaService.SavePicture(recipe.Id, recipe.Picture);
                recipe.ImageSource = await MediaService.GetPicture(recipe.Id, recipe.Picture);

                IEnumerable<Category> categories = Categories.Where(c => c.Recipes.Any(r => r.Id == recipe.Id));

                if (categories != null)
                {
                    Recipe oldRegister = categories.Select(c => c.Recipes.FirstOrDefault(r => r.Id == recipe.Id))
                        .FirstOrDefault();

                    oldRegister = recipe;
                }
            }
        }

        public async Task LoadFavorites()
        {
            if (Favorites.Any())
                return;

            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.ToListAsync();

                foreach (var category in categories)
                {
                    var recipes = await context.Recipes.
                        Where(r => r.CategoryId == category.Id
                        && r.Favorite)
                        .ToListAsync();

                    if (recipes != null && recipes.Any())
                    {
                        category.Recipes = recipes;

                        Favorites.Add(category);
                    }
                }
            }

            foreach (var category in Favorites)
            {
                foreach (var recipe in category.Recipes)
                {
                    recipe.ImageSource = await MediaService.GetPicture(recipe.Id, recipe.Picture);
                }
            }
        }

        public async Task<bool> Exists(Guid recipeId)
        {
            using (var context = new AppDbContext())
            {
                return await context.Recipes.AnyAsync(r => r.Id == recipeId);
            }
        }

        #region Seed Data
        private async Task Seed()
        {
            var cakesAndPies = new Category("Bolos e Tortas", string.Empty);
            var snacks = new Category("Lanches", string.Empty);
            var saladsAndSauces = new Category("Saladas e Molhos", string.Empty);
            var beef = new Category("Carnes", string.Empty);
            var chicken = new Category("Aves", string.Empty);
            var pasta = new Category("Massas", string.Empty);
            var soups = new Category("Sopas", string.Empty);
            var drinks = new Category("Bebidas", string.Empty);
            var fishes = new Category("Peixes", string.Empty);

            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.ToListAsync();
                if (categories.Any())
                    return;

                context.Categories.Add(cakesAndPies);
                context.Categories.Add(snacks);
                context.Categories.Add(saladsAndSauces);
                context.Categories.Add(beef);
                context.Categories.Add(chicken);
                context.Categories.Add(pasta);
                context.Categories.Add(soups);
                context.Categories.Add(drinks);
                context.Categories.Add(fishes);

                await context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
