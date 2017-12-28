using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Loader.ViewModels
{
    /// <summary>
    /// ViewModel handled by the Loader control
    /// </summary>
    public interface ILoaderViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Empty message
        /// </summary>
        string EmptyMessage { get; }

        /// <summary>
        /// Error message
        /// </summary>
        /// <value>The error message.</value>
        string ErrorMessage { get; }

        /// <summary>
        /// Current state of the Loader
        /// </summary>
        LoaderState State { get; set; }

        /// <summary>
        /// Cancels all executions run through <see cref="ExecuteAsync"/>
        /// </summary>
        void CancelExecutions();

        /// <summary>
        /// Executes a loading Task
        /// </summary>
        /// <param name="loadTask">Function that loads content wrapped by a Task</param>
        /// <param name="cancelPreviousExecutions">If set to <c>true</c> cancels previous executions</param>
        Task ExecuteAsync(Func<CancellationToken, Task<ILoaderResult>> loadTask, bool cancelPreviousExecutions = true);

        /// <summary>
        /// Refresh content by reexecuting the previous task
        /// </summary>
        /// <returns>The async.</returns>
        Task RefreshAsync();
    }
}