using Xamarin.Forms;

namespace Loader.Sample.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DefaultLoaderButton.Clicked += (sender, e) => Navigation.PushAsync(new DefaultPage());
            CustomLoaderButton.Clicked += (sender, e) => Navigation.PushAsync(new CustomPage());
        }
    }
}
