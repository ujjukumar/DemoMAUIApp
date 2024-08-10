﻿namespace DemoMAUIApp.ViewModels
{
    [QueryProperty(nameof(Monkey), "Monkey")]
    public partial class MonkeyDetailsViewModel : BaseViewModel
    {
        IMap map;
        public MonkeyDetailsViewModel(IMap map)
        {
            this.map = map;
        }

        [ObservableProperty]
        Monkey monkey;

        [RelayCommand]
        async Task OpenMapsAsync()
        {
            try
            {
                await map.OpenAsync(Monkey.Latitude, Monkey.Longitude,
                    new MapLaunchOptions
                    {
                        Name = Monkey.Name,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to open map application: {ex.Message}", "OK");
            }
        }
    }
}
