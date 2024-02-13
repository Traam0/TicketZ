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
        private DateTime _startTime;
        private DateTime _endTime;

        private decimal _advanceAmount;
        private decimal _amountReceived;
        private int _freeTime;
        private double _total;
        private int _manual_openings;
        private int _parked_vehicules;

        public int Id { get { return _id; } set { _id = value; } }
        public string Cashier { get { return _cashier; } set { if (value == _cashier) return; _cashier = value; } }
        public DateTime StartTime { get { return _startTime; } set { if (value == _startTime) return; _startTime = value; } }
        public DateTime EndTime { get { return _endTime; } set { if (value == _endTime) return; _endTime = value; } }
        public decimal AdvanceAmount { get { return _advanceAmount; } set { if (value == _advanceAmount) return; _advanceAmount = value; } }
        public decimal AmountReceived { get { return _amountReceived; } set { if (value == _amountReceived) return; _amountReceived = value; } }
        public int FreeTime { get { return _freeTime; } set { if (value == _freeTime) return; _freeTime = value; } }
        public decimal Total => this.AdvanceAmount + this.AmountReceived;
        public int ManualOpenings { get { return _manual_openings; } set { if (value == _manual_openings) return; _manual_openings = value; } }
        public int ParkedVehicules { get { return _parked_vehicules; } set { if (value == _parked_vehicules) return; _parked_vehicules = value; } }

    }
}
