using BlazingPizza.UI.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza.UI.Server.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private static UserInfo LoggedOutUser = new UserInfo { IsAuthenticated = false };

        [HttpGet("user")]
        public UserInfo GetUser()
        {
            return User.Identity.IsAuthenticated ? new UserInfo() { IsAuthenticated = true, Name = User.Identity.Name }  : LoggedOutUser;
        }

        [HttpGet("user/signin")]
        public async Task SignIn(string? redirectUri)
        {
            if(string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/home";
            }
            await HttpContext.ChallengeAsync(TwitterDefaults.AuthenticationScheme, new AuthenticationProperties() { RedirectUri = redirectUri });
        }

        [HttpGet("user/signout")]
        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/home");
        }


    }
}
