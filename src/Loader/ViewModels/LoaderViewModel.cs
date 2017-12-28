using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Loader.Helpers;
using Xamarin.Forms;

namespace Loader.ViewModels
{
    public class LoaderViewModel : NotificationObject, ILoaderViewModel
    {
        private LoaderState _state;
        public LoaderState State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        private string _emptyMessage;
        public string EmptyMessage
        {
            get => _emptyMessage;
            private set => SetProperty(ref _emptyMessage, value);
        }

        private Command _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand;

        private Func<CancellationToken, Task<ILoaderResult>> _loadTask;

        private readonly TaskRunner _taskRunner;

        public LoaderViewModel()
        {
            _taskRunner = new TaskRunner();

            _refreshCommand = new Command(() => RefreshAsync());
        }

        public async Task ExecuteAsync(Func<CancellationToken, Task<ILoaderResult>> loadTask, bool cancelPreviousTasks = true)
        {
            if (cancelPreviousTasks)
                CancelExecutions();

            _loadTask = loadTask;

            try
            {
                State = LoaderState.Loading;

                var result = await _taskRunner.ExecuteAsync(loadTask);

                if (result.IsEmpty)
                {
                    State = LoaderState.Empty;
                    return;
                }

                if (!result.IsSuccess)
                {
                    ErrorMessage = string.IsNullOrEmpty(result.ErrorMessage) ? "An error occurred!" : result.ErrorMessage;

                    State = LoaderState.Faulted;
                    return;
                }

                State = LoaderState.Completed;
            }
            catch (OperationCanceledException ex)
            {
                // Silently ignore the exception
            }
            catch (Exception ex)
            {
                State = LoaderState.Faulted;
                throw;
            }
        }

        public Task RefreshAsync()
        {
            if (_loadTask == null) return Task.CompletedTask;

            return ExecuteAsync(_loadTask);
        }

        public void CancelExecutions()
        {
            if (_loadTask == null) return;

            _taskRunner.CancelExecutions();
        }
    }
}
