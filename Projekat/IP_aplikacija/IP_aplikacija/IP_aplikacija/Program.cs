using IP_aplikacija.Client.Pages.Korisnik;
using IP_aplikacija.Client.States;
using IP_aplikacija.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<UgovorState>();

builder.Services.AddScoped(http => new HttpClient 
                                    { 
                                        BaseAddress = new Uri(builder.Configuration.GetSection("BaseURI").Value!) 
                                    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PretragaKorisnika).Assembly);

app.MapControllerRoute("default", "{controller}/{action}/{id?}");

app.Run();
