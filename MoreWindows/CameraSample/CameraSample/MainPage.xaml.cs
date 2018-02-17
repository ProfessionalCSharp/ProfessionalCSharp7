using System;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace CameraSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async void OnTakePhoto()
        {
            var cam = new CameraCaptureUI();
            cam.PhotoSettings.AllowCropping = true;
            cam.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Png;
            cam.PhotoSettings.CroppedSizeInPixels = new Size(300, 300);
            StorageFile file = await cam.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                var picker = new FileSavePicker();
                picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                picker.FileTypeChoices.Add("Image File", new string[] { ".png" });
                StorageFile fileDestination = await picker.PickSaveFileAsync();
                if (fileDestination != null)
                {
                    await file.CopyAndReplaceAsync(fileDestination);
                }
            }
        }

        public async void OnRecordVideo()
        {
            var cam = new CameraCaptureUI();
            cam.VideoSettings.AllowTrimming = true;
            cam.VideoSettings.MaxResolution =
              CameraCaptureUIMaxVideoResolution.StandardDefinition;
            cam.VideoSettings.Format = CameraCaptureUIVideoFormat.Wmv;
            cam.VideoSettings.MaxDurationInSeconds = 5;
            StorageFile file = await cam.CaptureFileAsync(
              CameraCaptureUIMode.Video);

            if (file != null)
            {
                var picker = new FileSavePicker();
                picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
                picker.FileTypeChoices.Add("Video File", new string[] { ".wmv" });
                StorageFile fileDestination = await picker.PickSaveFileAsync();
                if (fileDestination != null)
                {
                    await file.CopyAndReplaceAsync(fileDestination);
                }
            }
        }
    }
}
