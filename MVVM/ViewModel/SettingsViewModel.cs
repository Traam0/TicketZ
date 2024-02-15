using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicketZ.Core;
using TicketZ.Interfaces;

namespace TicketZ.MVVM.ViewModel
{
    public class SettingsViewModel: Core.ViewModel
    {
        private IRouter _router;

        public IRouter Router
        {
            get { return _router; }
            set
            {
                _router = value;
                OnPropertyChanged();
            }
        }


        private string _databaseHost;
        public string DatabaseHost { 
            get { return _databaseHost; }
            set 
            {
                if(value == _databaseHost) return;
                _databaseHost = value;
                OnPropertyChanged();
            }
        }
        private string _databaseUsername;
        public string DatabaseUsername
        {
            get { return _databaseUsername; }
            set
            {
                if(value == _databaseUsername) return;
                _databaseUsername = value;
                OnPropertyChanged();
            }
        }
        private string _databasePassword;
        public string DatabasePassword
        {
            get { return _databasePassword; }
            set 
            {
                if(value == _databasePassword) return;
                _databasePassword = value;
                OnPropertyChanged();
            }
        }
        private string _databaseName;
        public string DatabaseName
        {
            get { return _databaseName; }
            set 
            {
                if(value == _databaseName) return; 
                _databaseName = value; 
                OnPropertyChanged();
            }
        }

        public string? Error => _router?.Params?.GetValueOrDefault("error");
        public RelayCommand SaveCredentialsCommand {  get; set; }

        public SettingsViewModel(IRouter router)
        {
            var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            _router = router;
            _databaseHost = ConfigurationManager.AppSettings.Get("DataBaseHost")!;
            _databaseUsername = ConfigurationManager.AppSettings.Get("DataBaseUsername")!;
            _databasePassword = ConfigurationManager.AppSettings.Get("DataBasePassword")!;
            _databaseName = ConfigurationManager.AppSettings.Get("DataBaseName")!;
            SaveCredentialsCommand = new RelayCommand(x =>
            {
                ConfigurationManager.AppSettings.Set("DataBaseHost", "");
                ConfigurationManager.AppSettings.Set("DataBaseUsername", "");
                ConfigurationManager.AppSettings.Set("DataBasePassword", "");
                ConfigurationManager.AppSettings.Set("DatabaseName", "");
                MessageBox.Show("");

                DatabaseHost = ConfigurationManager.AppSettings.Get("DataBaseHost")!;
                DatabaseUsername= ConfigurationManager.AppSettings.Get("DataBaseUsername")!;
                DatabasePassword = ConfigurationManager.AppSettings.Get("DataBasePassword")!;
                DatabaseName = ConfigurationManager.AppSettings.Get("DataBaseName")!;
            }, x => true); ;
        }
    }
}
