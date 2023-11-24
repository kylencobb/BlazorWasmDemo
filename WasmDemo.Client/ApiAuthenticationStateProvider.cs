using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text;

namespace WasmDemo.Client
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public JwtAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("token");

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(GetClaimsIdentityFromJwt(token)));
        }

        public void SetUserAsAuthenticated(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(GetClaimsIdentityFromJwt(token));
            var authenticationState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authenticationState);
        }

        public void SetUserLoggedOut()
        {
            var emptyUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authenticationState = Task.FromResult(new AuthenticationState(emptyUser));

            NotifyAuthenticationStateChanged(authenticationState);
        }

        private static ClaimsIdentity GetClaimsIdentityFromJwt(string jwtToken)
        {
            var segments = jwtToken.Split('.');
            var payload = Encoding.UTF8.GetString(ParseBase64(segments[1]));
            var jsonData = JsonSerializer.Deserialize<JsonObject>(payload);

            var claims = new Claim[jsonData.Count];
            int index = 0;

            foreach (var record in jsonData)
            {
                claims[index] = JwtNodeToClaim(record.Key, record.Value);
                index++;
            }

            return new ClaimsIdentity(claims, "jwt");
        }

        private static Claim JwtNodeToClaim(string key, JsonNode node)
        {
            var jsonValue = node.AsValue();

            if (jsonValue.TryGetValue<string>(out var value))
                return new Claim(key, value, ClaimValueTypes.String);

            if (jsonValue.TryGetValue<double>(out var number))
                return new Claim(key, number.ToString(), ClaimValueTypes.Double);

            throw new Exception("Bad claim");
        }
        private static byte[] ParseBase64(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
