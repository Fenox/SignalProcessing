using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SignalGeneration;
using SignalGeneration.SignalProcessors;
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

        public RelayCommand ApplyGaußCommand { get; set; }

        public RelayCommand ApplyPrewitXCommand { get; set; }
        public RelayCommand ApplyPrewitYCommand { get; set; }
        public RelayCommand ApplyPrewitCommand { get; set; }

        public RelayCommand ApplySobelXCommand { get; set; }
        public RelayCommand ApplySobelYCommand { get; set; }
        public RelayCommand ApplySobelCommand { get; set; }

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

        SGImageSignalSource imageSource = new SGImageSignalSource(@"D:\OneDrive\Bilder\Icons\check-black-36px.bmp");

        public ImageProcessingViewModel()
        {            
            Image = BitmapToImageSource(imageSource.Image);

            ApplyGaußCommand = new RelayCommand(() => ExecuteFilter(new SGGausfilter()));

            ApplySobelXCommand = new RelayCommand(() => ExecuteFilter(new SGSobelX()));
            ApplySobelYCommand = new RelayCommand(() => ExecuteFilter(new SGSobelY()));
            ApplySobelCommand = new RelayCommand(() => ExecuteFilter(new SGSobel()));

            ApplyPrewitXCommand = new RelayCommand(() => ExecuteFilter(new SGPrewitXFilter()));
            ApplyPrewitYCommand = new RelayCommand(() => ExecuteFilter(new SGPrewitYFilter()));
            ApplyPrewitCommand = new RelayCommand(() => ExecuteFilter(new SGPrewit()));
        }

        void ExecuteFilter(ISignalProcessor<SGImageSignalSource, SGImageSignalSource> processor)
        {
            var res = processor.Process(imageSource);
           
            Image = BitmapToImageSource(res.Image);

            imageSource.Image.Dispose();
            imageSource = res; 

            RaisePropertyChanged("Image");
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
