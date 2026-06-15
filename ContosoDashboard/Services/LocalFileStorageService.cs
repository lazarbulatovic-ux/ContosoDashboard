using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _basePath;

        public LocalFileStorageService(IConfiguration config)
        {
            var configured = config["FileStorage:Local:BasePath"];
            _basePath = string.IsNullOrEmpty(configured)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContosoDashboard", "uploads")
                : configured;

            Directory.CreateDirectory(_basePath);
        }

        public async Task<string> UploadAsync(Stream fileStream, string relativePath, string contentType, CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(_basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            var dir = Path.GetDirectoryName(fullPath)!;
            Directory.CreateDirectory(dir);

            using var outStream = File.Create(fullPath);
            await fileStream.CopyToAsync(outStream, cancellationToken);
            return relativePath.Replace('\', '/');
        }

        public Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(_basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(fullPath)) File.Delete(fullPath);
            return Task.CompletedTask;
        }

        public Task<Stream> DownloadAsync(string relativePath, CancellationToken cancellationToken = default)
        {
            var fullPath = Path.Combine(_basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            Stream stream = File.OpenRead(fullPath);
            return Task.FromResult(stream);
        }

        public Task<string> GetUrlAsync(string relativePath, TimeSpan expiration)
        {
            // Local implementation does not provide public URLs; return file:// path for dev only
            var fullPath = Path.Combine(_basePath, relativePath.Replace('/', Path.DirectorySeparatorChar));
            return Task.FromResult(new Uri(fullPath).AbsoluteUri);
        }
    }
}
