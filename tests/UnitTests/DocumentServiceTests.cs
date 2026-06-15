using ContosoDashboard.Data;
using ContosoDashboard.Services;
using ContosoDashboard.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class DocumentServiceTests
    {
        private ApplicationDbContext CreateInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "docstest_db")
                .Options;
            return new ApplicationDbContext(options);
        }

        private class FileStorageStub : IFileStorageService
        {
            public Task DeleteAsync(string relativePath, System.Threading.CancellationToken cancellationToken = default) => Task.CompletedTask;
            public Task<Stream> DownloadAsync(string relativePath, System.Threading.CancellationToken cancellationToken = default) => Task.FromResult<Stream>(new MemoryStream());
            public Task<string> GetUrlAsync(string relativePath, System.TimeSpan expiration) => Task.FromResult($"file:///{relativePath}");
            public Task<string> UploadAsync(Stream fileStream, string relativePath, string contentType, System.Threading.CancellationToken cancellationToken = default)
            {
                return Task.FromResult(relativePath);
            }
        }

        [Fact]
        public async Task CreateAsync_SavesDocumentAndReturnsEntity()
        {
            using var db = CreateInMemoryDb();
            var storage = new FileStorageStub();
            var svc = new DocumentService(db, storage);

            using var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("hello"));
            var doc = await svc.CreateAsync(ms, "test.txt", "Test Doc", "General", 1, null, null, "text/plain");

            Assert.NotNull(doc);
            Assert.True(doc.DocumentId > 0);
            Assert.Equal("test.txt", doc.FileName);
        }
    }
}
