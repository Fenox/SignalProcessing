using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SignalGeneratorTestViewer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _contentViewModel;
        List<ViewModelBase> _pageViewModels = new List<ViewModelBase>();
        public List<ViewModelBase> PageViewModels => _pageViewModels ?? (_pageViewModels = new List<ViewModelBase>());
        public ICommand ChangePageCommand { get; set; }

        public MainViewModel()  
        {
            _pageViewModels.Add(new FourierViewModel());
            _pageViewModels.Add(new WienerProzessViewModel());
            _pageViewModels.Add(new ImageProcessingViewModel());
            _pageViewModels.Add(new BrownianMotionViewModel());

            ContentViewModel = _pageViewModels[0];

            ChangePageCommand = new RelayCommand<ViewModelBase>(
                      ChangeViewModel
                      );
        }

        private void ChangeViewModel(ViewModelBase viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            ContentViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

       

        public ViewModelBase ContentViewModel
        {
            get
            {
                return _contentViewModel;
            }
            set
            {
                if (_contentViewModel == value) return;

                _contentViewModel = value;
                RaisePropertyChanged("ContentViewModel");
            }
        }
    }
}