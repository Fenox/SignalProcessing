using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SignalGeneration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using SignalGeneration.SignalProcessors.Convolution;
using SignalGeneration.Common;

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
            Images.Add(new BitmapImage(new Uri("/SignalGeneratorTestViewer;component/Assets/check-white-36px.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("/SignalGeneratorTestViewer;component/Assets/CuteCat1.bmp", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("/SignalGeneratorTestViewer;component/Assets/CuteCat2.bmp", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("/SignalGeneratorTestViewer;component/Assets/CuteCat3.bmp", UriKind.Relative)));
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
            
        }

        void ExecuteFilter(ISignalProcessor<IsgTimeImageSignalSource, IsgTimeImageSignalSource> processor)
        {
            //Get a copy of the selected image and create new image, that can be operated on
            var imageSource = new IsgTimeImageSignalSource(Image.BitmapImage2Bitmap());            

            //Use the selected filter on the image
            IsgTimeImageSignalSource res = processor.Process(imageSource);
           
            //Change the image in the view
            Image = BitmapToImageSource(res.Image);

            //dispose the copied image
            imageSource.Image.Dispose();
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
