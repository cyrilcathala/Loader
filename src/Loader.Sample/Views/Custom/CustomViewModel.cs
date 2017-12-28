using System;
using System.Threading.Tasks;
using Loader.Sample.Toolkit;
using Loader.ViewModels;

namespace Loader.Sample.Views
{
    public class CustomViewModel : ViewModelBase
    {
        private int _counter = -1;

        public CustomViewModel()
        {
        }

        public override Task OnAppearing()
        {
            return LoadData();
        }

        public override Task OnDisappearing()
        {
            Loader.CancelExecutions();

            return Task.CompletedTask;
        }

        private Task LoadData()
        {
            return Loader.ExecuteAsync(async (token) =>
            {
                _counter = (_counter + 1) % 3;

                await Task.Delay(2000);

                switch (_counter)
                {
                    case 0:
                        return LoaderResult.Error("Oops, there was an error!");
                    case 1:
                        return LoaderResult.Empty();
                    default:
                        return LoaderResult.Success();
                }
            });
        }
    }
}
