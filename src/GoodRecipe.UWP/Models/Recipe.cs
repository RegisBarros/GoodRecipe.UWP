using System;

namespace GoodRecipe.UWP.Models
{
    public class Recipe
    {
        public Recipe(string titulo, string descricao, int tempoPreparo)
        {
            Titulo = titulo;
            Descricao = descricao;
            TempoPreparo = tempoPreparo;
        }

        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Ingredientes { get; set; }

        public string Preparo { get; set; }

        public string Foto { get; set; }

        public string Tags { get; set; }

        public bool Favorita { get; set; }

        /// <summary>
        /// Tempo de preparo em minutos
        /// </summary>
        public int TempoPreparo { get; set; }

        public Guid CategoriaId { get; set; }

        public Category Categoria { get; set; }
    }
}
