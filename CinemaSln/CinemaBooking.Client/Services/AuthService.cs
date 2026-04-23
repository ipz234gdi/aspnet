using System.Net.Http.Json;
using CinemaBooking.Client.Models;
using Microsoft.JSInterop;

namespace CinemaBooking.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly CustomAuthStateProvider _authProvider;

        public AuthService(HttpClient http, IJSRuntime js, CustomAuthStateProvider authProvider)
        {
            _http = http;
            _js = js;
            _authProvider = authProvider;
        }

        public async Task<string?> Login(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/account/login", request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                if (result != null)
                {
                    await _js.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);
                    _authProvider.NotifyAuthChanged();
                    return null;
                }
            }

            var error = await response.Content.ReadAsStringAsync();
            return error;
        }

        public async Task<string?> Register(RegisterRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/account/register", request);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            var error = await response.Content.ReadAsStringAsync();
            return error;
        }

        public async Task Logout()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
            _authProvider.NotifyAuthChanged();
        }

        public async Task<string?> GetToken()
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", "authToken");
        }
    }
}
