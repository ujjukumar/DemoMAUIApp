using System.Net.Http.Json;

namespace DemoMAUIApp.Services
{
    public class MonkeyService
    {
        readonly HttpClient _httpClient;

        public MonkeyService()
        {
            _httpClient = new HttpClient();
        }

        List<Monkey> monkeyList = [];

        public async Task<List<Monkey>> GetMonkeysFromUrlAsync()
        {
            if (monkeyList?.Count > 0)
            {
                return monkeyList;
            }

            string url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            }

            return monkeyList;
        }
    }
}
