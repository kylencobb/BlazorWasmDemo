using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WasmDemo.Shared;

namespace WasmDemo.Client.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/login", loginModel);

            var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();

            if (loginResult.IsSuccess)
            {
                await _localStorage.SetItemAsync("token", loginResult.Token);
                ((JwtAuthenticationStateProvider)_authenticationStateProvider).SetUserAsAuthenticated(loginResult.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            }

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((JwtAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
