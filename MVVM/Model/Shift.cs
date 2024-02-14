using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketZ.MVVM.Model
{
    public class Shift
    {
        private int _id;
        private string _cashier = string.Empty;
        private DateTime _startTime = default;
        private DateTime _endTime;

        private decimal _advanceAmount;
        private decimal _amountReceived;
        private int _freeTime;
        private double _total;
        private int _manualOpenings;
        private int _parkedVehicules;
        private int _lostTickets;

        public int Id { get { return _id; } set { _id = value; } }
        public string Cashier { get { return _cashier; } set { if (value == _cashier) return; _cashier = value; } }
        public DateTime StartTime { get { return _startTime; } set { if (value == _startTime) return; _startTime = value; } }
        public DateTime EndTime { get { return _endTime; } set { if (value == _endTime) return; _endTime = value; } }
        public decimal AdvanceAmount { get { return _advanceAmount; } set { if (value == _advanceAmount) return; _advanceAmount = value; } }
        public decimal AmountReceived { get { return _amountReceived; } set { if (value == _amountReceived) return; _amountReceived = value; } }
        public int FreeTime { get { return _freeTime; } set { if (value == _freeTime) return; _freeTime = value; } }
        public int ManualOpenings { get { return _manualOpenings; } set { if (value == _manualOpenings) return; _manualOpenings = value; } }
        public int ParkedVehicules { get { return _parkedVehicules; } set { if (value == _parkedVehicules) return; _parkedVehicules = value; } }
        public int LostTickets { get { return _lostTickets; } set { if (value == _lostTickets) return;  _lostTickets = value; } }
        public decimal Total => this.AdvanceAmount + this.AmountReceived + (this.LostTickets * 40);
        //TODO fix Price Multiplier
    }
}
