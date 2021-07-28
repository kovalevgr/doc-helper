using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Authentication;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using CabinetApp.Identity;
using DocHelper.Domain.Auth;
using DocHelper.Domain.Common.Services;

namespace CabinetApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiAuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
            ApiAuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts", loginModel);

            var loginResult = await result.Content.ReadFromJsonAsync<LoginResult>();

            if (!result.IsSuccessStatusCode)
            {
                return loginResult;
            }

            if (loginResult is null)
            {
                throw new AuthenticationException("");
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);

            _authenticationStateProvider.MarkUserAsAuthenticated(loginModel.Email);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");

            _authenticationStateProvider.MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);

            return await result.Content.ReadFromJsonAsync<RegisterResult>();
        }
    }
}