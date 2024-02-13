using Npgsql;
using System.Collections.Specialized;
using System.Configuration;
using TicketZ.Interfaces;
using TicketZ.MVVM.Model;
using TicketZ.MVVM.ViewModel;

namespace TicketZ.Repositories
{
    public class ShiftRepository
    {
        private IRouter _router;
        public event Action<IEnumerable<Shift>>? OnShiftsRetrived;
        private const int IPP = 10;
        private readonly string _connectionString;

        public ShiftRepository(IRouter router)
        {
            _router = router;
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            _connectionString = $"Host={appSettings.Get("DataBaseHost")};Username={appSettings.Get("DataBaseUsername")};Password={appSettings.Get("DataBasePassword")};Database={appSettings.Get("DatabaseName")}";
        }

        public async void RetreiveShifts(int page)
        {
            try
            {
                List<Shift> shifts = new();
                using NpgsqlConnection connection = new(_connectionString);
                await connection.OpenAsync();
                string commandQuery = $"SELECT * FROM ticketz LIMIT {IPP} OFFSET {IPP * (page - 1)}";
                using NpgsqlCommand getCommand = new(commandQuery, connection);
        
                using NpgsqlDataReader reader = await getCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    shifts.Add(new()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Cashier = reader.GetString(reader.GetOrdinal("cashier")),
                        AdvanceAmount = reader.GetDecimal(reader.GetOrdinal("advance_amount")),
                        AmountReceived = reader.GetDecimal(reader.GetOrdinal("amount_received")),
                        StartTime = reader.GetDateTime(reader.GetOrdinal("starttime")),
                        EndTime = reader.GetDateTime(reader.GetOrdinal("endtime")),
                        ParkedVehicules = reader.GetInt32(reader.GetOrdinal("parked_cars")),
                        ManualOpenings = reader.GetInt32(reader.GetOrdinal("manual_openings"))
                    });
                }

                OnShiftsRetrived?.Invoke(shifts);
            }
            catch (Exception ex)
            {
                _router.Push<SettingsViewModel>(new()
                {
                    {"error", ex.Message}
                });
            }
        }
    }
}
