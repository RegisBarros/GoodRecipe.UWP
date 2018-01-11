using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace GoodRecipe.UWP.Services
{
    public static class MediaService
    {
        public static async Task<byte[]> TakePicture()
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.AllowCropping = true;

            var storageFile = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            var fileBytes = await ReadFileBytes(storageFile);

            return fileBytes;
        }

        public static async Task<byte[]> OpenPicture()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            var storageFile = await picker.PickSingleFileAsync();
            var fileBytes = await ReadFileBytes(storageFile);

            return fileBytes;
        }

        private static async Task<byte[]> ReadFileBytes(Windows.Storage.StorageFile storageFile)
        {
            if (storageFile == null)
            {
                return null;
            }

            using (var stream = await storageFile.OpenReadAsync())
            {
                using (DataReader reader = new DataReader(stream))
                {
                    var imageBytes = new byte[stream.Size];

                    await reader.LoadAsync((uint)imageBytes.Length);
                    reader.ReadBytes(imageBytes);

                    return imageBytes;
                }
            }
        }
    }
}
