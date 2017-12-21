using System;
using System.Collections.ObjectModel;

namespace GoodRecipe.UWP.Models
{
    public class Category
    {
        public Category()
        {

        }

        public Category(string title, string description, ObservableCollection<Recipe> recipes)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Recipes = recipes;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ObservableCollection<Recipe> Recipes { get; set; }
    }
}
