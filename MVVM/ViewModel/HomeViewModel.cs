using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Compression;
using System.Windows.Data;
using System.Windows.Threading;
using TicketZ.Core;
using TicketZ.MVVM.Model;
using TicketZ.Repositories;

namespace TicketZ.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel, IDisposable
    {
        private readonly ShiftRepository _shfitRepository;
        private ObservableCollection<Shift> _shifts;
        public ObservableCollection<Shift> Shifts
        {
            get { return _shifts; }
            set
            {
                _shifts = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView ShiftsCollectionView { get; }

        private string _cahsierName;
        public string CashierName
        {
            get { return _cahsierName; }
            set
            {
                if (value == _cahsierName) return;
                _cahsierName = value;
                OnPropertyChanged();
                ShiftsCollectionView.Refresh();
            }
        }

        private int _page = 1;
        public int Page
        {
            get { return _page; }
            set
            {
                if (value <= 0 || _page == value) return;
                _page = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand GetNextPage {  get; set; }

        public HomeViewModel(ShiftRepository repository)
        {
            _shifts = new();
            _cahsierName = string.Empty;
            _shfitRepository = repository;

            _shfitRepository.OnShiftsRetrived += UpdateShiftCollection;

            GetNextPage = new(x =>
            {
                Page++;
                _shfitRepository.RetreiveShifts(Page);
            }, x => true);

            ShiftsCollectionView = CollectionViewSource.GetDefaultView(_shifts);
            ShiftsCollectionView.Filter = Filter;

            _shfitRepository.RetreiveShifts(1);
            Task.Run(() =>
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                    {
                    }
                );
            });
        }


        private void UpdateShiftCollection(IEnumerable<Shift> shifts)
        {
            foreach (Shift shift in shifts)
            {
                Shifts.Add(shift);

            }
        }

        private bool Filter(object @object)
        {
            if (@object is Shift shift)
            {
                return shift.Cashier.Contains(CashierName, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public void Dispose()
        {
            _shfitRepository.OnShiftsRetrived -= UpdateShiftCollection;
            GC.SuppressFinalize(this);
        }
    }
}
