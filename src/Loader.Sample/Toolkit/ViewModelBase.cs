using System.Threading.Tasks;
using Loader.ViewModels;

namespace Loader.Sample.Toolkit
{
    public class ViewModelBase : NotificationObject
    {
        private LoaderViewModel _loader;
        public LoaderViewModel Loader
        {
            get { return _loader; }
            set { SetProperty(ref _loader, value); }
        }

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private int _busyCounter;

        /// <summary>
        /// Counter to increment then decrement for each busy call, and linked with the <see cref="IsBusy"/> property
        /// </summary>
        protected int BusyCounter
        {
            get { return _busyCounter; }
            set
            {
                if (_busyCounter != value)
                {
                    _busyCounter = value;
                    RaisePropertyChanged(nameof(IsBusy));
                }
            }
        }

        /// <summary>
        /// Indicates whether this ViewModel is busy.
        /// </summary>
        /// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get { return BusyCounter > 0; }
        }

        public ViewModelBase()
        {
            _loader = new LoaderViewModel();
        }

        #region Life cycle

        public virtual Task OnAppearing()
        {
            return Task.FromResult(0);
        }

        public virtual Task OnDisappearing()
        {
            _loader.CancelExecutions();

            return Task.FromResult(0);
        }

        #endregion Life cycle
    }
}