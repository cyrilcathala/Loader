namespace Loader.ViewModels
{
    public class LoaderResult : ILoaderResult
    {
        public bool IsEmpty { get; private set; }
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public string EmptyMessage { get; private set; }

        private LoaderResult()
        {
        }

        public static ILoaderResult Success()
        {
            return new LoaderResult
            {
                IsSuccess = true
            };
        }

        public static ILoaderResult Error(string errorMessage = "")
        {
            return new LoaderResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }

        public static ILoaderResult Empty(string emptyMessage = "")
        {
            return new LoaderResult
            {
                IsEmpty = true,
                EmptyMessage = emptyMessage
            };
        }
    }
}
