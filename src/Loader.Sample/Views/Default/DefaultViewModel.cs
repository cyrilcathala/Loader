using System.Threading.Tasks;
using Loader.Sample.Toolkit;
using Loader.ViewModels;

namespace Loader.Sample.Views
{
    public class DefaultViewModel : ViewModelBase
    {
        private int _counter;

        public DefaultViewModel()
        {
        }

        public override Task OnAppearing()
        {
            return LoadData();
        }

        private Task LoadData()
        {
            return Loader.ExecuteAsync(async (token) =>
            {
                _counter = (_counter + 1) % 4;

                await Task.Delay(2000);

                if (_counter == 3)
                {
                    return LoaderResult.Success();
                }

                var countdown = 3 - _counter;
                return LoaderResult.Error($"Oops, there was an error! Try it {countdown} more time!");
            });
        }
    }
}
