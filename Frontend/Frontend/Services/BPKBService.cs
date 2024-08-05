using Frontend.Models;
using System.Text;

namespace Frontend.Services
{
    public class BPKBService
    {
        private readonly HttpClient _httpClient;

        public BPKBService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BackendApi");
        }

        public async Task<bool> ValidateUserAsync(MsUser model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/BPKB/login", model);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<TrBpkb>> GetAllTrBpkbsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TrBpkb>>($"api/BPKB/transactions");
        }

        public async Task<IEnumerable<MsStorageLocation>> GetAllLocationsAsync()
        {
            var response = await _httpClient.GetAsync("api/BPKB/locations"); // Adjust endpoint as needed
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<MsStorageLocation>>();
        }

        public async Task<TrBpkb?> GetTrBpkbAsync(string agreementNumber)
        {
            return await _httpClient.GetFromJsonAsync<TrBpkb>($"api/BPKB/transactions/{agreementNumber}");
        }

        public async Task<TrBpkb?> CreateTrBpkbAsync(TrBpkb bpkb)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/BPKB/transactions", bpkb);
                response.EnsureSuccessStatusCode(); // Throws an exception for HTTP error responses

                return await response.Content.ReadFromJsonAsync<TrBpkb>();
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"Error creating TrBpkb: {ex.Message}");
                return null;
            }
        }

        public async Task<TrBpkb?> UpdateTrBpkbAsync(TrBpkb bpkb)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/BPKB/transactions", bpkb);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TrBpkb>();
            }
            else
            {
                throw new HttpRequestException($"Error updating Transaction: {response.ReasonPhrase}");
            }
        }
    }
}
