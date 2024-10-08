using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class GeoLocationService
    {
        private readonly HttpClient _httpClient;

        public GeoLocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetCountryCodeByIP(string ipAddress)
        {
            var url = $"http://ip-api.com/json/{ipAddress}";
            var response = await _httpClient.GetFromJsonAsync<GeoLocationResponse>(url);

            if (response != null && response.Status == "success" && !string.IsNullOrEmpty(response.CountryCode))
            {
                return response.CountryCode;
            }

            return "Unknown";
        }
    }
}
