using Loader.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Loader.Controls
{
    /// <summary>
    /// Control to switch a content between different states (loading, error, empty, success)
    /// </summary>
    [ContentProperty("Child")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loader : ContentView
    {
        public View Child
        {
            get { return ContentContainer.Content; }
            set { ContentContainer.Content = value; }
        }

        public View EmptyView
        {
            get { return EmptyContainer.Content; }
            set { EmptyContainer.Content = value; }
        }

        public View ErrorView
        {
            get { return ErrorContainer.Content; }
            set { ErrorContainer.Content = value; }
        }

        public LoadingIndicatorBase LoadingIndicator
        {
            get { return LoadingIndicatorContainer.Content as LoadingIndicatorBase; }
            set { LoadingIndicatorContainer.Content = value; }
        }

        #region LoaderViewModel
        public static readonly BindableProperty LoaderViewModelProperty =
            BindableProperty.Create("LoaderViewModel",
                typeof(ILoaderViewModel),
                typeof(Loader),
                null,
                propertyChanged: (control, oldvalue, newvalue) => ((Loader)control).LoaderViewModelChanged());

        public ILoaderViewModel LoaderViewModel
        {
            get { return (ILoaderViewModel)GetValue(LoaderViewModelProperty); }
            set { SetValue(LoaderViewModelProperty, value); }
        }
        #endregion LoaderViewModel

        public Loader()
        {
            InitializeComponent();
        }

        private void LoaderViewModelChanged()
        {
            if (LoaderViewModel == null) return;

            LoaderViewModel.PropertyChanged += LoaderViewModel_PropertyChanged;
        }

        private void LoaderViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoaderViewModel.State))
            {
                StateChanged();
            }
        }

        private void StateChanged()
        {
            switch (LoaderViewModel.State)
            {
                case LoaderState.NotStarted:
                    break;

                case LoaderState.Loading:
                    ErrorContainer.IsVisible = false;
                    EmptyContainer.IsVisible = false;
                    ContentContainer.IsVisible = false;

                    LoadingIndicator.IsBusy = true;
                    break;

                case LoaderState.Completed:
                    ContentContainer.IsVisible = true;

                    LoadingIndicator.IsBusy = false;
                    break;

                case LoaderState.Faulted:
                    ErrorContainer.IsVisible = true;
                    ContentContainer.IsVisible = false;

                    LoadingIndicator.IsBusy = false;
                    break;

                case LoaderState.Empty:
                    EmptyContainer.IsVisible = true;
                    ContentContainer.IsVisible = false;

                    LoadingIndicator.IsBusy = false;
                    break;
            }
        }
    }
}