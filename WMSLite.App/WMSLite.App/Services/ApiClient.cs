using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WMSLite.App.Models;

namespace WMSLite.App.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ApiClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7247/") };
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<List<Contractor>> GetContractorsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Contractor>>("api/Contractors", _jsonSerializerOptions);

        public async Task<Contractor> AddContractorAsync(Contractor contractor)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Contractors", contractor);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Contractor>();
        }

        public async Task UpdateContractorAsync(int id, Contractor contractor)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Contractors/{id}", contractor);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<GoodsReceiptDocument>> GetGoodsReceiptDocumentsAsync() =>
            await _httpClient.GetFromJsonAsync<List<GoodsReceiptDocument>>("api/GoodsReceiptDocuments", _jsonSerializerOptions);

        public async Task<GoodsReceiptDocument> GetGoodsReceiptDocumentAsync(int id) =>
            await _httpClient.GetFromJsonAsync<GoodsReceiptDocument>($"api/GoodsReceiptDocuments/{id}", _jsonSerializerOptions);

        public async Task<GoodsReceiptDocument> AddGoodsReceiptDocumentAsync(GoodsReceiptDocument doc)
        {
            var response = await _httpClient.PostAsJsonAsync("api/GoodsReceiptDocuments", doc);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GoodsReceiptDocument>();
        }

        public async Task UpdateGoodsReceiptDocumentAsync(int id, GoodsReceiptDocument doc)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/GoodsReceiptDocuments/{id}", doc);
            response.EnsureSuccessStatusCode();
        }

        public async Task<DocumentItem> AddDocumentItemAsync(DocumentItem item)
        {
            var response = await _httpClient.PostAsJsonAsync("api/DocumentItems", item);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DocumentItem>();
        }

        public async Task UpdateDocumentItemAsync(int id, DocumentItem item)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/DocumentItems/{id}", item);
            response.EnsureSuccessStatusCode();
        }
    }
}