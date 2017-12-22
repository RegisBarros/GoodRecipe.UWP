using GoodRecipe.UWP.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodRecipe.UWP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GoodRecipe.db");
        }
    }
}
