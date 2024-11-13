using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Services.Implementations;

public class BlobstorageService : IBlobstorageService
{
    private readonly string _connectionString;
    private readonly string _containerName;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobstorageService(IConfiguration configuration)
    {
        _connectionString = configuration["AzureBlobStorage:ConnectionString"];
        _containerName = configuration["AzureBlobStorage:ContainerName"];
        _blobServiceClient = new BlobServiceClient(_connectionString);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
    }

    public async Task<string> UploadImageAsync(string fileName, Stream stream)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(stream, true);
        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<Stream> DownloadImageAsync(string fileName)
    {
        var blobClient = _blobContainerClient.GetBlobClient(fileName);
        var response = await blobClient.DownloadAsync();
        return response.Value.Content;
    }
}
