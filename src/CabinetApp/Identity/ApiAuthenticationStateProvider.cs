using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DocHelper.Domain.IdentityModel.Tokens;
using Microsoft.AspNetCore.Components.Authorization;

namespace CabinetApp.Identity
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly JwtTokenHandler _tokenHandler;

        public ApiAuthenticationStateProvider(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            JwtTokenHandler tokenHandler)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _tokenHandler = tokenHandler;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(_tokenHandler.GetDefaultClaimsPrincipal());
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(_tokenHandler.GetClaimsPrincipal(token));
        }
    }
}