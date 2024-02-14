using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MusicStore.Services.Interface;

namespace MusicStore.Services.Implementation
{
    public class FileStorageLocal : IFileStorage
    {
        private readonly ILogger<FileStorageLocal> logger;
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FileStorageLocal(ILogger<FileStorageLocal> logger, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            string databaseUrl = string.Empty;
            try
            {
                var fileName = $"{Guid.NewGuid()}{extension}";
                string folder = Path.Combine(env.WebRootPath, container);

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string path = Path.Combine(folder, fileName);
                await File.WriteAllBytesAsync(path, content);

                var currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                databaseUrl = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return databaseUrl;
        }
        public async Task<string> EditFile(byte[] content, string extension, string container, string path, string contentType)
        {
            await DeleteFile(path, container);
            return await SaveFile(content, extension, container, contentType);
        }
        public Task DeleteFile(string path, string container)
        {
            try
            {
                if (path is not null)
                {
                    var fileName = Path.GetFileName(path);
                    string fileDirectory = Path.Combine(env.WebRootPath, container, fileName);

                    if (File.Exists(fileDirectory))
                    {
                        File.Delete(fileDirectory);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Task.FromResult(0);
        }
    }
}
