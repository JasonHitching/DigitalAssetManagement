using Services.Implementations;
using Services.Abstractions;
using DigitalAssetManager.Components;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<IBlobStorageService>(provider => BlobStorageServiceFactory.Create(configuration));

        // Add interactive server components
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();

        var blobStorageService = app.Services.GetRequiredService<IBlobStorageService>();

        // Path to the image file to upload (TESTING ONLY)
        var filePath = "";
        var fileName = Path.GetFileName(filePath);

        // Upload the image (TESTING ONLY)
        using (var fileStream = File.OpenRead(filePath))
        {
            var uri = await blobStorageService.UploadImageAsync(fileName, fileStream);
            Console.WriteLine($"Image uploaded to: {uri}");
        }

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
    }
}