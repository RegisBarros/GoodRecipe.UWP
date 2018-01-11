using GoodRecipe.UWP.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodRecipe.UWP.Models
{
    public class Category : NotifyableClass
    {
        public Category()
        {
            Id = Guid.NewGuid();
        }

        public Category(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
