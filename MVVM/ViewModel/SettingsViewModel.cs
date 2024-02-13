using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketZ.Interfaces;

namespace TicketZ.MVVM.ViewModel
{
    public class SettingsViewModel: Core.ViewModel
    {
        private IRouter _router;

        public IRouter Router { 
            get { return _router; }
            set
            {
                _router = value;
                OnPropertyChanged();
            }
        }

        public string? Error => _router?.Params?.GetValueOrDefault("error");

        public SettingsViewModel(IRouter router)
        {
            _router = router;
        }
    }
}
