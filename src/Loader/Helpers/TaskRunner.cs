using System;
using System.Threading;
using System.Threading.Tasks;

namespace Loader.Helpers
{
    /// <summary>
    /// Wrapper to run and cancel tasks
    /// </summary>
    public class TaskRunner
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CancellationToken CancellationToken
        {
            get
            {
                return _cancellationTokenSource.Token;
            }
        }

        /// <summary>
        /// Executes a task with a given token which is cancelled with <see cref="CancelExecutions"/>
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="safeExecution">If true, it won't throw any exception and wrap them silently</param>
        public async Task ExecuteAsync(Func<CancellationToken, Task> action, bool safeExecution = false)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();
                await action(CancellationToken);
            }
            catch (Exception ex)
            {
                if (!safeExecution)
                    throw;
            }
        }

        /// <summary>
        /// Executes a task with a given token which is cancellable with <see cref="CancelExecutions"/>
        /// </summary>
        /// <param name="action">Action to be executed</param>
        /// <param name="safeExecution">If true, it won't throw any exception and wrap them silently</param>
        public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, bool safeExecution = false)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();
                return await action(CancellationToken);
            }
            catch (Exception ex)
            {
                if (!safeExecution)
                    throw;
            }

            return default(T);
        }

        /// <summary>
        /// Cancels all executions previously run through <see cref="ExecuteAsync"/>
        /// </summary>
        public void CancelExecutions()
        {
            if (!_cancellationTokenSource.IsCancellationRequested && CancellationToken.CanBeCanceled)
                _cancellationTokenSource.Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}