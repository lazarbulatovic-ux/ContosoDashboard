using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string relativePath, string contentType, CancellationToken cancellationToken = default);
        Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default);
        Task<Stream> DownloadAsync(string relativePath, CancellationToken cancellationToken = default);
        Task<string> GetUrlAsync(string relativePath, System.TimeSpan expiration);
    }
}
