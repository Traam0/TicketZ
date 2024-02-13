using System.Windows;
using TicketZ.Core;
using TicketZ.Interfaces;

namespace TicketZ.MVVM.ViewModel
{
    public class MainViewModel: Core.ViewModel
    {
        private IRouter _router;
        public string MyProperty { get; set; } = "Kiita";
        public IRouter Router
        {
            get => _router;
            set
            {
                _router = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand GoToHomeCommand { get; set; }
        public RelayCommand GoToSettingsCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }


        public MainViewModel(IRouter router)
        {
           _router = router;
            GoToHomeCommand = new(x =>
            {
                _router.Push<HomeViewModel>();
            }, x => true) ;

            GoToSettingsCommand = new(x =>
            {
                _router.Push<SettingsViewModel>();
            }, x => true);

            GoBackCommand = new(x => { _router.Goback(); }, x=>true);

            
        }



        
    }
}
