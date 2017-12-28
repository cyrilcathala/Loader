namespace Loader.ViewModels
{
    /// <summary>
    /// Loader result
    /// </summary>
    public interface ILoaderResult
    {
        bool IsEmpty { get; }
        bool IsSuccess { get; }
        string ErrorMessage { get; }
        string EmptyMessage { get; }
    }
}
