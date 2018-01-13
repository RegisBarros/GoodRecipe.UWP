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
                Recipes.Add(recipe);

                context.Recipes.Add(recipe);

                if (recipe.Category != null)
                {
                    context.Entry(recipe.Category).State = EntityState.Unchanged;
                }

                await context.SaveChangesAsync();
            }

            await MediaService.SavePicture(recipe.Id, recipe.Picture);
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

                    if (recipes != null)
                        category.Recipes = recipes;

                    Categories.Add(category);
                }
            }

            foreach (var category in Categories)
            {
                foreach (var recipe in category.Recipes)
                {
                    recipe.ImageSource = await MediaService.GetPicture(recipe.Id);
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

                    if (recipes != null)
                        category.Recipes = recipes;

                    Favorites.Add(category);
                }
            }

            foreach (var category in Favorites)
            {
                foreach (var recipe in category.Recipes)
                {
                    recipe.ImageSource = await MediaService.GetPicture(recipe.Id);
                }
            }
        }

        #region Seed Data
        private async Task Seed()
        {
            var cakesAndPies = new Category("Bolos e Tortas", string.Empty)
            {
                Recipes = GetRecipesForCakesAndPies()
            };

            var snacks = new Category("Lanches", string.Empty)
            {
                Recipes = GetRecipesForSnacks()
            };

            var saladsAndSauces = new Category("Saladas e Molhos", string.Empty)
            {
                Recipes = GetRecipesForSaladsAndSauces()
            };

            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.ToListAsync();
                if (categories.Any())
                    return;

                context.Categories.Add(cakesAndPies);
                context.Categories.Add(snacks);
                context.Categories.Add(saladsAndSauces);

                await context.SaveChangesAsync();
            }
        }

        private ICollection<Recipe> GetRecipesForCakesAndPies()
        {
            return new List<Recipe>
            {
                new Recipe("Torta de palmito", "Torta fácil de preparar para o seu dia a dia", 40),
                new Recipe("Quiche de alho poró", "Escelente receita de quiche de alho poró", 20)
            };
        }

        private ICollection<Recipe> GetRecipesForSnacks()
        {
            return new List<Recipe>
            {
                new Recipe("Empadão de Frango Especial", "Empadão de Frango Especial", 90),
                new Recipe("WRAP", "Sanduíche WRAP", 30)
            };
        }

        private ICollection<Recipe> GetRecipesForSaladsAndSauces()
        {
            return new List<Recipe>
            {
                new Recipe("Arroz Especial", "Faça um delicioso arroz", 60)
            };
        }
        #endregion
    }
}
