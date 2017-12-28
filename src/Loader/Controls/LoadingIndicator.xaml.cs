using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Loader.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingIndicator : LoadingIndicatorBase
    {
        public LoadingIndicator()
        {
            InitializeComponent();
        }

        protected override void IsBusyChanged()
        {
            if (IsBusy) StartLoading();
            else StopLoading();
        }

        public async Task StartLoading()
        {
            Loading.IsVisible = true;
            Loading.IsRunning = true;

            Overlay.IsVisible = true;
            await Overlay.FadeTo(1, 250);
        }

        public async Task StopLoading()
        {
            Loading.IsVisible = false;
            Loading.IsRunning = false;

            await Overlay.FadeTo(0, 250);
            Overlay.IsVisible = false;
        }
    }
}
