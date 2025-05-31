using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Pelikello.App.Authentication;

public class AuthenticationHandler(IHttpContextAccessor httpContextAccessor)
{
  private HttpContext? HttpContext => httpContextAccessor.HttpContext;

  public async Task SignInAsync()
  {
    if (HttpContext is null)
    {
      return;
    }

    var claims = new List<Claim> { new(ClaimTypes.Name, "root") };
    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
  }

  public async Task SignOutAsync()
  {
    if (HttpContext is null)
    {
      return;
    }

    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
  }
}
