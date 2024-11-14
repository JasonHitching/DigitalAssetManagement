using Services.Implementations;
using Services.Abstractions;
using DigitalAssetManager.Components;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IBlobStorageService, BlobStorageService>();

// Add interactive server components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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