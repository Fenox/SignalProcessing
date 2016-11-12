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
using SignalGeneration.SignalProcessors.Convolution;

namespace SignalGeneratorTestViewer.ViewModel
{
    public class ImageProcessingViewModel : ViewModelBase, IControlViewModel
    {
        public string Name => "Image Processing";

        public RelayCommand ApplyGaußCommand { get; set; }

        public RelayCommand ApplyPrewitXCommand { get; set; }
        public RelayCommand ApplyPrewitYCommand { get; set; }
        public RelayCommand ApplyPrewitCommand { get; set; }

        public RelayCommand ApplySobelXCommand { get; set; }
        public RelayCommand ApplySobelYCommand { get; set; }
        public RelayCommand ApplySobelCommand { get; set; }


        public RelayCommand ApplyRobertsCommand { get; set; }

        public RelayCommand ApplyLaplaceCommand { get; set; }

        public RelayCommand ImageChangedCommand { get; set; }

        List<BitmapImage> _images = new List<BitmapImage>();
        public List<BitmapImage> Images
        {
            get
            {
                return _images;
            }
            set
            {
                _images = value;
                RaisePropertyChanged("Images");
            }
        }

        BitmapImage _image;
        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
            }
        }

        

        public ImageProcessingViewModel()
        {
            Images.Add(new BitmapImage(new Uri(@"D:\OneDrive\Bilder\Icons\check-black-36px.bmp")));
            Images.Add(new BitmapImage(new Uri(@"C:\Users\Julian\Pictures\CuteCats\LowResolution\CuteCat1.bmp")));
            Images.Add(new BitmapImage(new Uri(@"C:\Users\Julian\Pictures\CuteCats\LowResolution\CuteCat2.bmp")));
            Images.Add(new BitmapImage(new Uri(@"C:\Users\Julian\Pictures\CuteCats\LowResolution\CuteCat3.bmp")));
            Image = Images[0];

            ApplyGaußCommand = new RelayCommand(() => ExecuteFilter(new SGGausfilter()));

            ApplySobelXCommand = new RelayCommand(() => ExecuteFilter(new SGSobelX()));
            ApplySobelYCommand = new RelayCommand(() => ExecuteFilter(new SGSobelY()));
            ApplySobelCommand = new RelayCommand(() => ExecuteFilter(new SGSobel()));

            ApplyPrewitXCommand = new RelayCommand(() => ExecuteFilter(new SGPrewitXFilter()));
            ApplyPrewitYCommand = new RelayCommand(() => ExecuteFilter(new SGPrewitYFilter()));
            ApplyPrewitCommand = new RelayCommand(() => ExecuteFilter(new SGPrewit()));

            ApplyRobertsCommand = new RelayCommand(() => ExecuteFilter(new SGRoberts()));

            ApplyLaplaceCommand = new RelayCommand(() => ExecuteFilter(new SGLaplaceFilter()));

            ImageChangedCommand = new RelayCommand(ChangeImage);
        }

        void ChangeImage()
        {

        }

        void ExecuteFilter(ISignalProcessor<SGImageSignalSource, SGImageSignalSource> processor)
        {
            var imageSource = new SGImageSignalSource(Image.UriSource.AbsolutePath);
            SGImageSignalSource res = processor.Process(imageSource);
           
            Image = BitmapToImageSource(res.Image);

            imageSource.Image.Dispose();

            RaisePropertyChanged("Image");
        }

        private BitmapImage BitmapToImageSource(Image bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
