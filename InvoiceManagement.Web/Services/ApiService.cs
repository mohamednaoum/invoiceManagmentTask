using System.Net.Http.Headers;
using InvoiceManagement.Application.DTOs;
using InvoiceManagement.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceManagement.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { username, password });
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return result["token"];
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetInvoicesAsync()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("api/invoice");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<InvoiceViewModel>>();
        }

        public async Task<bool> CreateInvoiceAsync(InvoiceViewModel model)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJsonAsync("api/invoice", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"api/invoice/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<InvoiceDto> GetInvoiceByIdAsync(int id)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _httpClient.GetFromJsonAsync<InvoiceDto>($"api/invoices/{id}");
        }
        public async Task<IEnumerable<SelectListItem>> GetStoresAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("api/store");
            response.EnsureSuccessStatusCode();

            var stores = await response.Content.ReadFromJsonAsync<IEnumerable<StoreViewModel>>();
            return stores.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
        }

        public async Task<IEnumerable<SelectListItem>> GetItemsAsync()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("api/item");
            response.EnsureSuccessStatusCode();

            var items = await response.Content.ReadFromJsonAsync<IEnumerable<ItemViewModel>>();
            return items.Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name });
        }

        
    }
}