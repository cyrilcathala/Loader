using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Loader.Sample.Views
{
    public partial class CustomPage : ContentPage
    {
        public CustomViewModel ViewModel
        {
            get { return BindingContext as CustomViewModel; }
            private set { BindingContext = value; }
        }

        public CustomPage()
        {
            InitializeComponent();

            ViewModel = new CustomViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.OnDisappearing();
        }
    }
}
