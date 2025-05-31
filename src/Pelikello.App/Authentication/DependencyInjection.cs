using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pelikello.App.Authentication;

public static class DependencyInjection
{
  public static IServiceCollection AddPelikelloAuthorization(this IServiceCollection services)
  {
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie("Cookies", options =>
        {
          options.Cookie.Name = "PelikelloAuth";
          options.LoginPath = "/login";
          options.LogoutPath = "/logout";
          options.AccessDeniedPath = "/access-denied";
          options.ExpireTimeSpan = TimeSpan.FromDays(30);
        });

    services.AddAuthorization();
    services.AddHttpContextAccessor();
    services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    services.AddScoped<AuthenticationHandler>();

    return services;
  }

  public static WebApplication UsePelikelloAuthorization(this WebApplication app)
  {
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapRoutes();

    return app;
  }

  private static WebApplication MapRoutes(this WebApplication app)
  {
    app.MapPost("/auth/login", async (
      HttpContext context,
      AuthenticationHandler auth,
      [FromBody] LoginModel model,
      PelikelloOptions options) =>
    {
      if (model.Passcode == options.Passcode)
      {
        await auth.SignInAsync();
        return Results.Ok();
      }

      return Results.BadRequest();
    });

    app.MapPost("/auth/logout", async (
      AuthenticationHandler auth) =>
    {
      await auth.SignOutAsync();
      return Results.Ok();
    });

    return app;
  }
}
