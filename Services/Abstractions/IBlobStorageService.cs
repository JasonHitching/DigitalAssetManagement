namespace Services.Abstractions
{
    /// <summary>
    /// Interface for blob storage service to handle image upload and download operations.
    /// </summary>
    public interface IBlobStorageService
    {
        /// <summary>
        /// Uploads an image to the blob storage.
        /// </summary>
        /// <param name="fileName">The name of the file to be uploaded.</param>
        /// <param name="stream">The stream containing the image data.</param>
        /// <returns>The task result contains the URL of the uploaded image.<returns>
        Task<string> UploadImageAsync(string fileName, Stream stream);

        /// <summary>
        /// Downloads an image from the blob storage.
        /// </summary>
        /// <param name="fileName">The name of the file to be downloaded.</param>
        /// <returns>The task result contains the stream of the downloaded image.</returns>
        Task<Stream> DownloadImageAsync(string fileName);
    }
}
