<<<<<<< Updated upstream:Program.cs
using DigitalAssetManager.Components;
=======
using Services.Implementations;
using Services.Abstractions;
>>>>>>> Stashed changes:DigitalAssetManager/Program.cs

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
<<<<<<< Updated upstream:Program.cs
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
=======
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();
>>>>>>> Stashed changes:DigitalAssetManager/Program.cs

var app = builder.Build();

var blobstorageService = app.Services.GetRequiredService<IBlobStorageService>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
