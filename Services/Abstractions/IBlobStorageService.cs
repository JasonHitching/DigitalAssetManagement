namespace Services.Abstractions;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(string fileName, Stream stream);
    Task<Stream> DownloadImageAsync(string fileName);
}
