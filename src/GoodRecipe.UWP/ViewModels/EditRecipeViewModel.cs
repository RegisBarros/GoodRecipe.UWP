using GoodRecipe.UWP.Abstracts;
using GoodRecipe.UWP.Data.Repositories;
using GoodRecipe.UWP.Models;
using GoodRecipe.UWP.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


namespace GoodRecipe.UWP.ViewModels
{
    public class EditRecipeViewModel : NotifyableClass
    {
        public CategoryRepository CategoryRepository { get; private set; } = CategoryRepository.Instance;

        public RecipeRepository RecipeRepository { get; private set; } = RecipeRepository.Instance;

        public ObservableCollection<Category> Categories { get; private set; }

        private Recipe _recipe;

        public Recipe Recipe
        {
            get { return _recipe; }
            set { Set(ref _recipe, value); }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get
            {
                if (_imageSource != null)
                    return _imageSource;

                LoadImage();

                return _imageSource;
            }
            set { Set(ref _imageSource, value); }
        }

        public async Task Initialize()
        {
           await CategoryRepository.LoadAll();

            Categories = CategoryRepository.Categories;            
        }

        public async void SaveRecipeButton_Click()
        {
            await RecipeRepository.Create(Recipe);
        }

        public async void OpenPictureButton_Click()
        {
            var picture = await MediaService.OpenPicture();

            if (picture != null)
            {
                Recipe.Picture = picture;

                LoadImage();
            }
        }

        public async void CameraButton_Click()
        {
            var picture = await MediaService.TakePicture();

            if (picture != null)
            {
                Recipe.Picture = picture;
                LoadImage();
            }
        }

        private async Task LoadImage()
        {
            if (Recipe.Picture == null || Recipe.Picture.Length == 0)
                return;

            using (var ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(Recipe.Picture);
                    await writer.StoreAsync();
                }

                var image = new BitmapImage();
                image.DecodePixelHeight = 200;

                await image.SetSourceAsync(ms);

                ImageSource = image;
            }
        }
    }
}
