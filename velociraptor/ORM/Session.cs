using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace velociraptor.ORM
{
    public class Session
    {
        public static async void SetCookies(string identity, HttpContext context)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identity)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
