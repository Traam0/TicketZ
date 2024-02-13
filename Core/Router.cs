using TicketZ.Interfaces;

namespace TicketZ.Core
{
    public class Router: ObservableObject, IRouter
    {
        private Dictionary<string, string>? _routeParmas;
        private Func<Type, ViewModel> _viewFactory;
        private Stack<ViewModel> _navigatorStack;
        private ViewModel _currentView;

        public ViewModel CurrentView
        {
            get => _currentView;
            set
            {
                if(null == value || _currentView == value) return;
                _navigatorStack.Push(_currentView);     
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<string, string>? Params => _routeParmas;

        public Router(Func<Type, ViewModel> viewfactory)
        {
            _navigatorStack = new Stack<ViewModel>();
            _viewFactory = viewfactory;
        }

        public void Push<T>() where T : ViewModel
        {
            _routeParmas = null;
            CurrentView = _viewFactory.Invoke(typeof(T));
        }

        public void Push<T>(Dictionary<string, string>? @params) where T : ViewModel
        {   
            _routeParmas = @params;
            CurrentView = _viewFactory.Invoke(typeof(T));
        }

        public void Goback()
        {
            if(_navigatorStack.Count > 0)
            {
                _currentView = _navigatorStack.Pop();
                OnPropertyChanged(nameof(CurrentView));
            }
        }
    }
}
