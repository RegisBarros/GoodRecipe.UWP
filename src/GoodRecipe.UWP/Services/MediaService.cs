using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace GoodRecipe.UWP.Services
{
    public static class MediaService
    {
        private static readonly string _folderName = "Pictures";

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

        public static async Task SavePicture(Guid recipeId, byte[] image)
        {
            string fileName = GetFileName(recipeId);

            StorageFolder pictureFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(_folderName, CreationCollisionOption.OpenIfExists);
            var file = await pictureFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            StorageFile storageFile = await image.AsStorageFile(fileName);
            WriteableBitmap writeableBitmap = await GetWriteableBitmap(storageFile);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream.AsRandomAccessStream());
                var pixelStream = writeableBitmap.PixelBuffer.AsStream();
                byte[] pixels = new byte[writeableBitmap.PixelBuffer.Length];

                await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)writeableBitmap.PixelWidth, (uint)writeableBitmap.PixelHeight, 96, 96, pixels);

                await encoder.FlushAsync();
            }
        }

        public static async Task<BitmapImage> GetPicture(Guid recipeId, byte[] picture, bool force = true)
        {
            string fileName = GetFileName(recipeId);

            StorageFolder storageFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync(_folderName);

            try
            {
                StorageFile storageFile = await storageFolder.GetFileAsync(fileName);

                using (IRandomAccessStream fileStream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = 200;

                    await bitmapImage.SetSourceAsync(fileStream);
                    return bitmapImage;
                }
            }
            catch (FileNotFoundException ex)
            {
                if (force)
                {
                    await SavePicture(recipeId, picture);

                    return await GetPicture(recipeId, picture, false);
                }

                return null;

            }
            catch
            {
                return null;
            }
        }

        private static async Task<byte[]> ReadFileBytes(StorageFile storageFile)
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

        private static async Task<StorageFile> AsStorageFile(this byte[] byteArray, string fileName)
        {
            StorageFolder storageFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync(_folderName);
            StorageFile storageFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(storageFile, byteArray);
            return storageFile;
        }

        private static async Task<WriteableBitmap> GetWriteableBitmap(StorageFile storageFile)
        {
            var stream = await storageFile.OpenReadAsync();
            var wb = new WriteableBitmap(1, 1);
            await wb.SetSourceAsync(stream);
            return wb;
        }

        private static string GetFileName(Guid recipeId)
        {
            return $"{recipeId}.jpg";
        }
    }
}
