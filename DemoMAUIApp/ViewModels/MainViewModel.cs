using DemoMAUIApp.Services;

namespace DemoMAUIApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        readonly MonkeyService _monkeyService;
        public ObservableCollection<Monkey> Monkeys { get; } = [];

        public MainViewModel(MonkeyService monkeyService)
        {
            Title = "Monkey Finder";
            this._monkeyService = monkeyService;
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        {
            if (IsBusy) return;

            try
            {
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
    }
}
