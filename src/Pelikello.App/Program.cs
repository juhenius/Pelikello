using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pelikello.App;
using Pelikello.App.Authentication;
using Pelikello.App.Components;
using Pelikello.App.Data;
using Pelikello.App.SystemActions;
using Pelikello.App.Timing;
using Pelikello.App.Tracking;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration.GetRequiredSection("Pelikello").Get<PelikelloOptions>()
  ?? throw new Exception("Configuration is missing");

builder.Services.AddRazorComponents()
  .AddInteractiveServerComponents();
builder.Services.AddDbContextFactory<PelikelloDbContext>((provider, options) =>
    options.UseSqlite(config.Db)
      .AddInterceptors([provider.GetRequiredService<TrackingEventsInterceptor>()]));
builder.Services.AddHttpClient("api", options =>
{
  options.BaseAddress = new Uri("/");
});
builder.Services
  .AddSingleton(config)
  .AddPelikelloAuthorization()
  .AddPelikelloSystemActions()
  .AddPelikelloTracking()
  .AddPelikelloTiming();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PelikelloDbContext>>();
  var db = await dbFactory.CreateDbContextAsync();
  await db.Database.MigrateAsync();
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.UsePelikelloAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
