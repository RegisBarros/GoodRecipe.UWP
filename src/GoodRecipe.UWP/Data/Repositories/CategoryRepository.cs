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
    public class CategoryRepository : NotifyableClass, IRepository<Category>
    {
        private static readonly Lazy<CategoryRepository> _instance = new Lazy<CategoryRepository>(() => new CategoryRepository());

        public static CategoryRepository Instance { get { return _instance.Value; } }

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();

        public ObservableCollection<Category> Categories
        {
            protected set { Set(ref _categories, value); }
            get { return _categories; }
        }

        public Task Create(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task LoadAll()
        {
            if (Categories.Count > 0)
            {
                return;
            }

            using (var context = new AppDbContext())
            {
                var categories = await context.Categories.ToListAsync();

                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
        }

        public Task Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
