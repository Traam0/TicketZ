using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketZ.Core;

namespace TicketZ.Interfaces
{
    public interface IRouter
    {
        ViewModel CurrentView { get; }
        Dictionary<string, string>? Params { get; }

        void Push<T>() where T:  ViewModel;
        void Push<T>(Dictionary<string, string> @params) where T:  ViewModel;
        void Goback();
    }
}
