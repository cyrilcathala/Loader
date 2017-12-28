using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Loader.Sample.Views
{
    public partial class DefaultPage : ContentPage
    {
        public DefaultViewModel ViewModel
        {
            get { return BindingContext as DefaultViewModel; }
            private set { BindingContext = value; }
        }

        public DefaultPage()
        {
            InitializeComponent();

            ViewModel = new DefaultViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OnAppearing();
        }
    }
}
