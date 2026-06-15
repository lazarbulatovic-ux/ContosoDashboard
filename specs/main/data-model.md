# Data Model: Document Upload and Management

## Entities

- Document
  - DocumentId: int (PK)
  - Title: string (required)
  - Description: string (optional)
  - Category: string (required)
  - FileName: string (original filename, for display)
  - FilePath: string (relative storage path)
  - ContentType: string (MIME, 255 chars)
  - SizeBytes: long
  - UploadedById: int (FK -> Users)
  - AssociatedProjectId: int? (FK -> Projects)
  - Tags: string (comma-separated or normalized tag table)
  - ScanStatus: string/enum (Pending, Available, Rejected)
  - ScanMetadata: json/text (scanner name, timestamp, verdict)
  - CreatedAt: datetime

- DocumentShare
  - DocumentShareId: int (PK)
  - DocumentId: int (FK)
  - GranteeUserId: int? (FK)
  - GranteeTeamId: int? (FK)
  - GrantedById: int (FK)
  - GrantedAt: datetime

## Indexes

- Index on `UploadedById` for `My Documents` queries
- Composite index on `AssociatedProjectId, Category, CreatedAt` for project
  views and filtering
- Full-text index (or scoped search index) on `Title`, `Description`, `Tags`
