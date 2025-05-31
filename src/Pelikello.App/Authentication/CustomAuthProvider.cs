using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Pelikello.App.Authentication;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private HttpContext? HttpContext => _httpContextAccessor.HttpContext;

  public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public override Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    var user = HttpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());
    return Task.FromResult(new AuthenticationState(user));
  }
}
