# Loader control for Xamarin.Forms

The Loader is a MVVM-oriented control to display different views depending on a specific loading state, for Xamarin.Forms specifically.

## Loading states

The loading state is handled by a LoaderViewModel:

* **Loading**: work is in progress, such as data being retrieved, the control displays a loading indicator
* **Completed**: work is done, displays the specified content
* **Faulted**: work could not be completed, displays an error view
* **Empty**: work is completed but no data retrieved, displays an empty view

![Loading](images/Loader_Loading.png?s=100)
![Completed](images/Loader_Completed.png?s=100)
![Faulted](images/Loader_Faulted.png?s=100)
![Empty](images/Loader_Empty.png?s=100)

The list of states is currently not extensible, to focus on generic states.
This might evolve in the future, especially with [VisualStates](https://github.com/xamarin/Xamarin.Forms/pull/1405) coming soon.

## Features

* **Customizable**: loading indicator, error and empty views can be customized
* **Refresh**: a RefreshCommand is available to trigger a refresh based on the last execution
* **Cancellable execution**: every execution can be cancelled, and if a new execution is triggered, any previous execution is cancelled (by default)

## Quickstart

Install from NuGet:

[![NuGet](https://img.shields.io/nuget/v/Loader.svg?label=NuGet)](https://www.nuget.org/packages/Loader/)

Then, reference the Loader control and put your content inside:

```xml
<ContentPage
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:controls="clr-namespace:Loader.Controls;assembly=Loader"
    x:Class="Loader.Sample.Views.DefaultPage">
    <controls:Loader
        LoaderViewModel="{Binding Loader}">
        <Label
            VerticalOptions="Center"
            HorizontalTextAlignment="Center"
            Text="Yay, successfully loaded!"
            FontSize="Large" />
    </controls:Loader>
</ContentPage>
```

The Loader control is MVVM oriented and works together with a LoaderViewModel:

```csharp
public class DefaultViewModel : NotificationObject
{
    private readonly LoaderViewModel _loader;
    public LoaderViewModel Loader => _loader;
    
    public DefaultViewModel()
    {
        _loader = new LoaderViewModel();
    }
    
    public Task LoadData()
    {
        return Loader.ExecuteAsync(async (token) =>
        {
            // Http call or work to be done
            var result = await _webService.MakeCallAsync();

            if (!result.IsSuccess)
            {
                 return LoaderResult.Error($"Oops, there was an error!");
            }
            
            if (result.Content == null) 
            {
            		return LoaderResult.Empty("Nothing to see here :(").
            }

            return LoaderResult.Success();
        });
    }    
}    
```

Every async execution must be wrapped into the **Loader.ExecuteAsync** and return a **ILoaderResult** corresponding to the state to be displayed.

Please note:

* It is a best practice to support CancellationToken in your execution path, you are encouraged to do so.
* If your execution throws an exception, it will be catched and converted into a Faulted state.

## Advanced usage

### Samples

Samples are provided in the sources with example usages of default and custom views.

### Custom loading indicator

Your custom loading indicator have to implement the LoadingIndicatorBase:

```csharp
public partial class LoadingIndicator : LoadingIndicatorBase
    {
        public LoadingIndicator()
        {
            InitializeComponent();
        }

        protected override void IsBusyChanged()
        {
            if (IsBusy)
               Loading.IsRunning = true;
            else
               Loading.IsRunning = false;
        }
    }
```

```xml
<controls:LoadingIndicatorBase
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:controls="clr-namespace:Loader.Controls"
    x:Class="Loader.Controls.LoadingIndicator">
    <ActivityIndicator
        HorizontalOptions="Center"
        VerticalOptions="Center"
        x:Name="Loading"/>
</controls:LoadingIndicatorBase>
```

### Custom error and empty views

```xml
<controls:Loader
    LoaderViewModel="{Binding Loader}">
    <!-- ErrorView -->
    <controls:Loader.ErrorView>
        <StackLayout
            VerticalOptions="Center">
            <Label
                Margin="0,12,0,0"
                Text="{Binding Loader.ErrorMessage}"
                FontSize="Small"
                HorizontalTextAlignment="Center" />
            <Button
                BackgroundColor="#FF6666"
                TextColor="White"
                Margin="12"
                Command="{Binding Loader.RefreshCommand}"
                Text=" Try again! " />
        </StackLayout>
    </controls:Loader.ErrorView>
    <controls:Loader.EmptyView>
        <StackLayout
            VerticalOptions="Center">
            <Label
                HorizontalTextAlignment="Center"
                Text="Empty..."
                FontSize="26" />
            <Label
                HorizontalTextAlignment="Center"
                Text="Nothing to see :("
                FontSize="Small" />
        </StackLayout>
    </controls:Loader.EmptyView>
    <!-- Normal content -->
</controls:Loader>
```

