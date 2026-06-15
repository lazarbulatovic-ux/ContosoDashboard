using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoDashboard.Models
{
    public enum ScanStatus
    {
        Pending,
        Available,
        Rejected
    }

    public class Document
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public string Category { get; set; } = string.Empty;

        public string? FileName { get; set; }

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public string? ContentType { get; set; }

        public long SizeBytes { get; set; }

        public int UploadedById { get; set; }

        public int? AssociatedProjectId { get; set; }

        public string? Tags { get; set; }

        public ScanStatus ScanStatus { get; set; } = ScanStatus.Pending;

        public string? ScanMetadata { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
