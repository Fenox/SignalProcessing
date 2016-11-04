using GalaSoft.MvvmLight;
using SignalGeneration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace SignalGeneratorTestViewer.ViewModel
{
    public class ImageProcessingViewModel : ViewModelBase, IControlViewModel
    {
        public string Name
        {
            get
            {
                return "Image Processing";
            }
        }

        BitmapImage image;
        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }

        public ImageProcessingViewModel()
        {
            SGImageSignalSource imageSource = new SGImageSignalSource(@"C:\Users\Julian\Pictures\Cute Cats\Posted\4985410560_22c77a17d9_b.jpg");

            Image = BitmapToImageSource(imageSource.Image);            
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
