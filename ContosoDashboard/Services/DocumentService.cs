using ContosoDashboard.Data;
using ContosoDashboard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public interface IDocumentService
    {
        Task<Document> CreateAsync(Stream fileStream, string originalFileName, string title, string category, int uploadedById, int? projectId, string? tags, string contentType, CancellationToken ct = default);
    }

    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileStorageService _storage;

        public DocumentService(ApplicationDbContext db, IFileStorageService storage)
        {
            _db = db;
            _storage = storage;
        }

        public async Task<Document> CreateAsync(Stream fileStream, string originalFileName, string title, string category, int uploadedById, int? projectId, string? tags, string contentType, CancellationToken ct = default)
        {
            // generate path: {userId}/{projectId or personal}/{guid}.{ext}
            var ext = Path.GetExtension(originalFileName);
            var guid = Guid.NewGuid().ToString();
            var folder = projectId.HasValue ? projectId.Value.ToString() : "personal";
            var relativePath = Path.Combine(uploadedById.ToString(), folder, guid + ext).Replace('\\','/');

            // store file
            await _storage.UploadAsync(fileStream, relativePath, contentType, ct);

            var doc = new Document
            {
                Title = title,
                Description = null,
                Category = category,
                FileName = originalFileName,
                FilePath = relativePath,
                ContentType = contentType,
                SizeBytes = fileStream.CanSeek ? fileStream.Length : 0,
                UploadedById = uploadedById,
                AssociatedProjectId = projectId,
                Tags = tags,
                ScanStatus = ScanStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _db.Documents.Add(doc);
            await _db.SaveChangesAsync(ct);
            return doc;
        }
    }
}
