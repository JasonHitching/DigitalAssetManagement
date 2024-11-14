using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Services.Abstractions;

namespace Services.Implementations;

public class BlobStorageService : IBlobStorageService
{
    private readonly string _containerName;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    internal BlobStorageService(string containerName, BlobServiceClient blobServiceClient, BlobContainerClient blobContainerClient)
    {
        _containerName = containerName;
        _blobServiceClient = blobServiceClient;
        _blobContainerClient = blobContainerClient;
    }

    // <inheritdoc />
    public async Task<string> UploadImageAsync(string fileName, Stream stream)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(stream, true);
        return blobClient.Uri.AbsoluteUri;
    }

    // <inheritdoc />
    public async Task<Stream> DownloadImageAsync(string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        var response = await blobClient.DownloadAsync();
        return response.Value.Content;
    }
}

public static class BlobStorageServiceFactory
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static BlobStorageService Create(IConfiguration configuration)
    {
        var containerName = configuration["AzureBlobStorage:ContainerName"] ?? throw new ArgumentNullException("AzureBlobStorage:ContainerName");
        var blobServiceUri = configuration["AzureBlobStorage:BlobServiceUri"] ?? throw new ArgumentNullException("AzureBlobStorage:BlobServiceUri");

        var blobServiceClient = new BlobServiceClient(new Uri(blobServiceUri), new DefaultAzureCredential());
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

        return new BlobStorageService(containerName, blobServiceClient, blobContainerClient);
    }
}
