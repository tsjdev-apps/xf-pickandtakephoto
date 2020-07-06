using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFPickAndTakePhoto
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void PickPhotoButtonOnClicked(object sender, EventArgs e)
        {
            // initialie
            await CrossMedia.Current.Initialize();

            // pick photo
            var mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium, CompressionQuality = 90 });
            if (mediaFile == null)
                return;

            // show image
            PickedImage.Source = ImageSource.FromStream(() => mediaFile.GetStream());
        }

        private async void TakePhotoButtonOnClicked(object sender, EventArgs e)
        {
            // initialize
            await CrossMedia.Current.Initialize();

            // check if camera is available
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                return;

            // take photo
            var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { Directory = "XFPickAndTakePhoto", Name = $"{Guid.NewGuid()}.jpg", SaveToAlbum = true, SaveMetaData = true });
            if (mediaFile == null)
                return;

            // show image
            PickedImage.Source = ImageSource.FromStream(() => mediaFile.GetStream());
        }
    }
}
