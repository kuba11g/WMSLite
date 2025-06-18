using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using WMSLite.App.Models;

namespace WMSLite.App.Services
{
    internal class ApiService
    {
        private readonly HttpClient httpClient;
        private const string BaseApiUrl = "https://localhost:7034/";

        public ApiService()
        {
            httpClient = new HttpClient { BaseAddress = new(BaseApiUrl) };
        }

        public Task<List<Contractor>> GetContractorsAsync() => httpClient.GetFromJsonAsync<List<Contractor>>("api/contractors");
        public async Task AddContractorAsync(Contractor contractor) => await httpClient.PostAsJsonAsync("api/contractors", contractor);
        public async Task UpdateContractorAsync(Contractor contractor) => await httpClient.PutAsJsonAsync($"api/contractors/{contractor.Id}", contractor);
    }
}
