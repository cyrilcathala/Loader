using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace Loader.Sample.Controls
{
    public class SvgImage : SvgCachedImage
    {
        #region SvgPath

        public static readonly BindableProperty SvgPathProperty =
            BindableProperty.Create("SvgPath",
                typeof(string),
                typeof(SvgImage),
                string.Empty,
                propertyChanged: (currentControl, oldValue, newValue) =>
                {
                    var svgImage = currentControl as SvgImage;
                    svgImage.SvgPath = (string)newValue;
                    svgImage.OnSvgPathChanged();
                });

        public string SvgPath
        {
            get { return (string)GetValue(SvgPathProperty); }
            set
            {
                SetValue(SvgPathProperty, value);
            }
        }

        #endregion SvgPath

        private void OnSvgPathChanged()
        {
            if (SvgPath == null) return;

            var assemblyName = GetType().Assembly.GetName().Name;
            var svgPathNormalized = SvgPath.Replace("/", ".");
            var svgFullPath = $"{assemblyName}.{svgPathNormalized}";

            Source = SvgImageSource.FromResource(svgFullPath);
        }
    }
}