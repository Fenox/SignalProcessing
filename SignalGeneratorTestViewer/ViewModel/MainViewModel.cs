using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SignalGeneratorTestViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase contentViewModel;
        List<ViewModelBase> pageViewModels { get; set; } = new List<ViewModelBase>();
        
        public ICommand ChangePageCommand { get; set; }

        public MainViewModel()  
        {
            pageViewModels.Add(new FourierViewModel());
            pageViewModels.Add(new WienerProzessViewModel());
            pageViewModels.Add(new ImageProcessingViewModel());
            pageViewModels.Add(new BrownianMotionViewModel());

            ContentViewModel = pageViewModels[0];

            ChangePageCommand = new RelayCommand<ViewModelBase>(
                      p => ChangeViewModel(p)
                      );
        }

        private void ChangeViewModel(ViewModelBase viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            ContentViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public List<ViewModelBase> PageViewModels
        {
            get
            {
                if (pageViewModels == null)
                    pageViewModels = new List<ViewModelBase>();

                return pageViewModels;
            }
        }

        public ViewModelBase ContentViewModel
        {
            get
            {
                return contentViewModel;
            }
            set
            {
                if (contentViewModel != value)
                {
                    contentViewModel = value;
                    RaisePropertyChanged("ContentViewModel");
                }
            }
        }
    }
}