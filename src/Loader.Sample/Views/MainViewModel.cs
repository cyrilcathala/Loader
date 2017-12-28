using System.Threading.Tasks;
using Loader.Sample.Toolkit;
using Loader.ViewModels;

namespace Loader.Sample
{
    public class MainViewModel : ViewModelBase
    {
        private int _counter;

        public MainViewModel()
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

                return LoaderResult.Error("Oops, there was an error!");
            });
        }
    }
}