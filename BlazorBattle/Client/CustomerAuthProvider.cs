using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorBattle.Client.Service;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorBattle.Client
{
    public class CustomerAuthProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private readonly IBananaService _bananaService;

        public CustomerAuthProvider(ILocalStorageService localStorageService, HttpClient http,IBananaService bananaService
        )
        {
            _localStorageService = localStorageService;
            _http = http;
            _bananaService = bananaService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    await _bananaService.GetBananas();
                }
                catch (Exception)
                {
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var pairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = pairs.Select(x => new Claim(x.Key, x.Value.ToString()));
            return claims;
        }
    }
}