# Research: Document Upload and Management

Decisions and rationale for open questions in the feature spec.

1. Malware scanning approach
   - Decision: Asynchronous quarantine (files stored in `pending_scan` state,
     scanned by a background worker, moved to `available` on success).
   - Rationale: Keeps upload latency low while ensuring no file is available
     prior to scanning.

2. Storage abstraction
   - Decision: Implement `IFileStorageService` with `LocalFileStorageService`
     for training and a future `AzureBlobStorageService` for production.
   - Rationale: Allows seamless migration without schema or business logic
     changes.

3. File path strategy
   - Decision: `{userId}/{projectId or personal}/{guid}.{ext}` using GUIDs for
     filenames; store paths relative to app data directory outside `wwwroot`.

4. Database choices
   - DocumentId: integer (consistent with existing schema). Category: text.

5. Testing and frameworks
   - Use xUnit for unit tests; use bUnit for Blazor component testing for the
     UI. Integration tests via TestServer or WebApplicationFactory.
