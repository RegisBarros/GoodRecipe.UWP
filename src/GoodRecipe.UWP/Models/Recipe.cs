using GoodRecipe.UWP.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public Category Category { get; set; }

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
    }
}
