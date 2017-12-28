using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Loader.Controls
{
    /// <summary>
    /// Loading indicator used by the Loader Control
    /// </summary>
    public abstract class LoadingIndicatorBase : ContentView
    {
        #region IsBusy
        public static readonly BindableProperty IsBusyProperty =
            BindableProperty.Create(
                "IsBusy",
                typeof(bool),
                typeof(LoadingIndicatorBase),
                default(bool),
                propertyChanged: (control, oldvalue, newvalue) => ((LoadingIndicatorBase)control).IsBusyChanged());

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        #endregion IsBusy

        protected abstract void IsBusyChanged();
    }
}