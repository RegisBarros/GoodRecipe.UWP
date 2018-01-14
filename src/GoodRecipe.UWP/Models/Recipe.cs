using GoodRecipe.UWP.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Windows.UI.Xaml.Media;

namespace GoodRecipe.UWP.Models
{
    public class Recipe : NotifyableClass
    {
        public Recipe()
        {
            Id = Guid.NewGuid();
        }

        public Recipe(string title, string description, double readyInTime)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            ReadyInTime = readyInTime;
        }

        private Guid _id;

        [Key]
        public Guid Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private Guid _categoryId;
        public Guid CategoryId
        {
            get
            {
                if (Category != null)
                    return Category.Id;

                return _categoryId;
            }
            set { Set(ref _categoryId, value); }
        }


        private Category _category;

        public Category Category
        {
            get { return _category; }
            set { Set(ref _category, value); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _ingredients;

        public string Ingredients
        {
            get { return _ingredients; }
            set { Set(ref _ingredients, value); }
        }

        private string _directions;

        public string Directions
        {
            get { return _directions; }
            set { Set(ref _directions, value); }
        }

        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { Set(ref _picture, value); }
        }

        private string _tags;

        public string Tags
        {
            get { return _tags; }
            set { Set(ref _tags, value); }
        }

        private bool _favorite;

        public bool Favorite
        {
            get { return _favorite; }
            set { Set(ref _favorite, value); }
        }

        private double _readyInTime;

        public double ReadyInTime
        {
            get { return _readyInTime; }
            set { Set(ref _readyInTime, value); }
        }

        private ImageSource _imageSource;

        [NotMapped]
        public ImageSource ImageSource
        {
            get
            {
                if (_imageSource != null)
                    return _imageSource;

                return _imageSource;
            }
            set { Set(ref _imageSource, value); }
        }

        [NotMapped]
        public List<string> Messages { get; set; } = new List<string>();

        [NotMapped]
        public bool IsValid => !Messages.Any();

        public void Validate()
        {
            Messages.Clear();

            if (Picture == null)
                Messages.Add("Adicione uma foto");

            if (string.IsNullOrEmpty(Title))
                Messages.Add("Informe o titulo");

            if (string.IsNullOrEmpty(Description))
                Messages.Add("Infrome a descrição");

            if (Category == null)
                Messages.Add("Selecione um categoria");

            if (ReadyInTime == 0)
                Messages.Add("Informe o tempo de preparo");

            if (string.IsNullOrEmpty(Ingredients))
                Messages.Add("Informe os ingredientes");

            if (string.IsNullOrEmpty(Directions))
                Messages.Add("Informe o modo de preparo");
        }
    }
}
