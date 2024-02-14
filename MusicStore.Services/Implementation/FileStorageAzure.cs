using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MusicStore.Services.Interface;

namespace MusicStore.Services.Implementation
{
    public class FileStorageAzure : IFileStorage
    {
        private readonly ILogger<FileStorageAzure> logger;
        private readonly string azureConnectionString;

        public FileStorageAzure(ILogger<FileStorageAzure> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.azureConnectionString = configuration.GetConnectionString("AzureStorage") ?? string.Empty;
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            string blobUri = string.Empty;
            try
            {
                var client = new BlobContainerClient(azureConnectionString, container);
                await client.CreateIfNotExistsAsync();
                client.SetAccessPolicy(PublicAccessType.Blob);

                var fileName = $"{Guid.NewGuid()}{extension}";
                var blob = client.GetBlobClient(fileName);

                var blobUploadOptions = new BlobUploadOptions();
                var blobHttpHeader = new BlobHttpHeaders();
                blobHttpHeader.ContentType = contentType;
                blobUploadOptions.HttpHeaders = blobHttpHeader;

                await blob.UploadAsync(new BinaryData(content), blobUploadOptions);
                blobUri = blob.Uri.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return blobUri;
        }
        public async Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType)
        {
            await DeleteFile(path, container);
            return await SaveFile(content, extension, container, contentType);

        }
        public async Task DeleteFile(string path, string container)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    return;

                var client = new BlobContainerClient(azureConnectionString, container);
                await client.CreateIfNotExistsAsync();
                var file = Path.GetFileName(path);
                var blob = client.GetBlobClient(file);
                await blob.DeleteIfExistsAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
