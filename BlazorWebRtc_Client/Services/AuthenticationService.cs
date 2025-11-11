using Blazored.LocalStorage;
using BlazorWebRtc_Client.Models.Response;
using System.Net.Http.Headers;

namespace BlazorWebRtc_Client.Services
{
    public class AuthenticationService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private const string TOKEN_KEY = "authToken";
        private const string USER_KEY = "currentUser";

        public AuthenticationService(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _localStorage.GetItemAsStringAsync(TOKEN_KEY);
            }
            catch
            {
                return null;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetItemAsStringAsync(TOKEN_KEY, token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task SetUserAsync(object user)
        {
            await _localStorage.SetItemAsync(USER_KEY, user);
        }

        public async Task<T?> GetUserAsync<T>()
        {
            try
            {
                return await _localStorage.GetItemAsync<T>(USER_KEY);
            }
            catch
            {
                return default(T);
            }
        }

        public async Task ClearAuthDataAsync()
        {
            await _localStorage.RemoveItemAsync(TOKEN_KEY);
            await _localStorage.RemoveItemAsync(USER_KEY);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task InitializeAuthAsync()
        {
            var token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<bool> LoginAsync(ResponseModel response)
        {
            if (response?.IsSuccess == true && !string.IsNullOrEmpty(response.Data?.ToString()))
            {
                await SetTokenAsync(response.Data.ToString()!);
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await ClearAuthDataAsync();
        }
    }
}
