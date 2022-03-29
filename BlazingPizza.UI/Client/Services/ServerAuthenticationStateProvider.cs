using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using BlazingPizza.UI.Shared;
using System.Net.Http.Json;

namespace BlazingPizza.UI.Client.Services
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient HttpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var UserInfo = await HttpClient.GetFromJsonAsync<UserInfo>("user");
            if (UserInfo.IsAuthenticated)
            {
                var claim = new Claim(ClaimTypes.Name, UserInfo.Name);
                var Identity = new ClaimsIdentity(new[] { claim }, "serverauth");
                return new AuthenticationState(new ClaimsPrincipal(Identity));
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }    

            

        }
    }
}
