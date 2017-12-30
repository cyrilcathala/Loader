using System.Threading.Tasks;
using Loader.ViewModels;

namespace Loader.Sample.Toolkit
{
    public class ViewModelBase : NotificationObject
    {
        private readonly LoaderViewModel _loader;
        public LoaderViewModel Loader => _loader;

        private string title = string.Empty;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
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