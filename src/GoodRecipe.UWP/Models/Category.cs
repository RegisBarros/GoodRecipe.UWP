using System;
using System.Collections.Generic;

namespace GoodRecipe.UWP.Models
{
    public class Category
    {
        public Category()
        {
        }

        public Category(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
