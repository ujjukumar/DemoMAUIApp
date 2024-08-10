using DemoMAUIApp.Services;

namespace DemoMAUIApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        readonly IConnectivity _connectivity;
        readonly IGeolocation _geolocation;
        readonly MonkeyService _monkeyService;
        public ObservableCollection<Monkey> Monkeys { get; } = [];

        public MainViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Monkey Finder";
            this._monkeyService = monkeyService;
            this._connectivity = connectivity;
            this._geolocation = geolocation;
        }

        [RelayCommand]
        async Task GoToMonkeyDetailsAsync(Monkey monkey)
        {
            if (monkey is null) return;

            await Shell.Current.GoToAsync(nameof(MonkeyDetailsPage), true,
                new Dictionary<string, object>
                {
                    {nameof(Monkey), monkey}
                });
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        {
            if (IsBusy) return;

            try
            {
                if (_connectivity.NetworkAccess is not NetworkAccess.Internet)
                {
                    Debug.WriteLine("Device is not connected to Internet.");

                    await Shell.Current.DisplayAlert("Internet Issue",
                        $"Check your internet and try again please!", "OK");

                    return;
                }

                IsBusy = true;

                var monkeys = await _monkeyService.GetMonkeysFromUrlAsync();

                if (Monkeys.Count is not 0)
                {
                    Monkeys.Clear();
                }

                foreach (var monkey in monkeys)
                {
                    Monkeys.Add(monkey);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get Monkeys: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GetClosestMonkeyAsync()
        {
            if (IsBusy || Monkeys.Count is 0) return;

            try
            {
                var location = await _geolocation.GetLastKnownLocationAsync();

                if (location is null || location.Timestamp > DateTimeOffset.Now.AddMinutes(30))
                {
                    try
                    {
                        location = await _geolocation.GetLocationAsync(
                            new GeolocationRequest
                            {
                                DesiredAccuracy = GeolocationAccuracy.Best,
                                Timeout = TimeSpan.FromSeconds(20)
                            });
                    }
                    catch (Exception ex)
                    {
                        await Shell.Current.DisplayAlert("Error!",
                            $"Unable to get device location. \n{ex.Message}", "OK");
                        return;
                    }
                }

                if (location is null) return;

                var firstMonkey = Monkeys.OrderBy(m =>
                    location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers))
                    .FirstOrDefault();

                if (firstMonkey is null) return;

                await Shell.Current.DisplayAlert("Closest Monkey",
                    $"{firstMonkey.Name} in {firstMonkey.Location}", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                await Shell.Current.DisplayAlert("Error!",
                    $"Unable to get closest monkey: {ex.Message}", "OK");
            }
        }
    }
}
