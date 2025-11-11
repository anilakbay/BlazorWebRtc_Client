using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorWebRtc_Client.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationService _authService;

        public CustomAuthStateProvider(ILocalStorageService localStorage, AuthenticationService authService)
        {
            _localStorage = localStorage;
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var isAuthenticated = await _authService.IsAuthenticatedAsync();
                
                if (isAuthenticated)
                {
                    var token = await _authService.GetTokenAsync();
                    var user = await _authService.GetUserAsync<object>();
                    
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user?.ToString() ?? "User"),
                        new Claim("Token", token ?? "")
                    };

                    var identity = new ClaimsIdentity(claims, "apiauth_type");
                    var userPrincipal = new ClaimsPrincipal(identity);

                    return new AuthenticationState(userPrincipal);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auth state error: {ex.Message}");
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim("Token", token)
            };

            var identity = new ClaimsIdentity(claims, "apiauth_type");
            var userPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userPrincipal)));
        }

        public void NotifyUserLogout()
        {
            var identity = new ClaimsIdentity();
            var userPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userPrincipal)));
        }
    }
}
