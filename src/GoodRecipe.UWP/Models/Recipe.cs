using System;

namespace GoodRecipe.UWP.Models
{
    public class Recipe
    {
        public Recipe(string title, string description, int readyInTime)
        {
            Title = title;
            Description = description;
            ReadyInTIme = readyInTime;
        }

        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public string Directions { get; set; }

        public string Picture { get; set; }

        public string Tags { get; set; }

        public bool Favorite { get; set; }

        /// <summary>
        /// Tempo de preparo em minutos
        /// </summary>
        public int ReadyInTIme { get; set; }
    }
}
