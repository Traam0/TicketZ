using Serilog;
using TicketZ.Core;
using System.Windows;
using TicketZ.Interfaces;
using TicketZ.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Diagnostics;
using Npgsql;
using System.Configuration;
using TicketZ.Repositories;

namespace TicketZ
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TicketZ/Log", "logs.txt"))
                .CreateLogger();

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton(Log.Logger);
            services.AddSingleton(ViewFactory);
            services.AddSingleton<IRouter, Router>();
            services.AddSingleton<ShiftRepository>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton(sp => new MainWindow()
            {
                DataContext = sp.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SettingsViewModel>();

            _serviceProvider = services.BuildServiceProvider();


            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                _serviceProvider.GetRequiredService<ILogger>().Fatal(e.ExceptionObject as Exception, "Unhandled exception");
                Log.CloseAndFlush();
            };
        }

        ViewModel ViewFactory(Type viewModelType)
        {
            return (ViewModel)_serviceProvider.GetRequiredService(viewModelType);
        }

        void Initialize()
        {

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                using NpgsqlConnection conn = new(
                    $"Host={appSettings.Get("DataBaseHost")};Username={appSettings.Get("DataBaseUsername")};Password={appSettings.Get("DataBasePassword")};Database={appSettings.Get("DatabaseName")}"
                    );

                conn.Open();
                using NpgsqlCommand existsCommand = new("SELECT EXISTS ( SELECT 1 FROM information_schema.tables WHERE table_schema = 'public' AND TABLE_NAME ilike 'TicketZ')", conn);

                bool? tableExists = existsCommand.ExecuteScalar() as bool?;

                if (tableExists == null || tableExists == false)
                {
                    Log.Logger.Information("Table TicktZ Not found, initializing...");
                    using NpgsqlCommand createCmd = new(
                           "CREATE TABLE TicketZ (id SERIAL PRIMARY KEY,cashier TEXT,advance_amount NUMERIC,amount_received NUMERIC, manual_openings INTEGER, starttime TIMESTAMP, endtime TIMESTAMP, lost_tickets INTEGER, parked_cars INTEGER)",
                           conn
                           );

                    createCmd.ExecuteNonQuery();
                    Log.Logger.Information("Table TicktZ created");
                }

                _serviceProvider.GetRequiredService<IRouter>().Push<HomeViewModel>();

            }
            catch (NpgsqlException ex)
            {
                Log.Logger.Error("Error while Initializing app, code: {code}, reason : {message}", ex.ErrorCode, ex.Message);
                _serviceProvider.GetRequiredService<IRouter>().Push<SettingsViewModel>(new Dictionary<string, string>()
                {
                    {"error", ex.Message}
                });

            }
            catch (Exception ex)
            {
                Log.Logger.Error("Error while Initializing app, error_message: {message}", ex.Message);
                MessageBox.Show($"Failed To Initialize app. \n {ex.Message} ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //todo Initialize DB, AppConfig
            Log.Logger.Information("Application opened");

            Initialize();

            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();


            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Logger.Information("Application closed");

            base.OnExit(e);
        }
    }

}
