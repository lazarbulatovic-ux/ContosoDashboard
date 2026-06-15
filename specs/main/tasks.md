---

description: "Task list for Document Upload and Management feature"
---

# Tasks: Document Upload and Management

**Input**: Design documents from `specs/001-document-upload-management/`  
**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`  

## Phase 1: Setup (Shared Infrastructure)

- [ ] T001 Create feature docs and placeholders in `specs/001-document-upload-management/`
- [ ] T002 Initialize project configuration for file storage in `appsettings.json` and `appsettings.Development.json` (`ContosoDashboard/appsettings.json`)
- [ ] T003 [P] Add CI job to run unit and integration tests and static analysis (`.github/workflows/ci.yml`)
- [ ] T004 [P] Configure local uploads directory and permission guidance (`README.md` and quickstart: `specs/main/quickstart.md`)

---

## Phase 2: Foundational (Blocking Prerequisites)

- [ ] T005 [P] Create `Document` entity in `ContosoDashboard/Data/Document.cs`
- [ ] T006 [P] Create `DocumentShare` entity in `ContosoDashboard/Data/DocumentShare.cs`
- [ ] T007 [P] Add `IFileStorageService` interface in `ContosoDashboard/Services/IFileStorageService.cs`
- [ ] T008 [P] Implement `LocalFileStorageService` in `ContosoDashboard/Services/LocalFileStorageService.cs` (store files outside `wwwroot`)
- [ ] T009 [P] Implement `DocumentService` skeleton in `ContosoDashboard/Services/DocumentService.cs` (upload, metadata operations)
- [ ] T010 Implement background scanning worker `ContosoDashboard/Services/DocumentScanWorker.cs` and queue integration
- [ ] T011 [P] Update `ContosoDashboard/Data/ApplicationDbContext.cs` to include `DbSet<Document>` and `DbSet<DocumentShare>` and create EF Core migration in `Migrations/`
- [ ] T012 Configure structured logging and audit fields in `Program.cs` and `ContosoDashboard/appsettings.json`
- [ ] T013 [P] Add unit test project scaffolding and initial tests for `DocumentService` (`tests/unit/DocumentServiceTests.cs`)

---

## Phase 3: User Story 1 - Upload Document (Priority: P1) 🎯 MVP

**Goal**: Allow users to upload files with metadata; files are scanned before availability.

**Independent Test**: Upload a supported file ≤25MB with required metadata and verify it appears in `My Documents` and is downloadable after scan completes.

- [ ] T014 [P] [US1] Create upload UI component `ContosoDashboard/Shared/Components/DocumentUpload.razor`
- [ ] T015 [US1] Create Upload page `ContosoDashboard/Pages/Documents/Upload.razor`
- [ ] T016 [US1] Implement server-side controller/endpoint to accept uploads and return upload status `ContosoDashboard/Controllers/DocumentController.cs`
- [ ] T017 [US1] Integrate `DocumentService` with `LocalFileStorageService` to perform the save-to-disk -> create-db-record workflow (`ContosoDashboard/Services/DocumentService.cs`)
- [ ] T018 [US1] Show upload progress and validation in UI (max size/type) (`ContosoDashboard/Shared/Components/DocumentUpload.razor`)
- [ ] T019 [US1] Add integration test for upload flow (`tests/integration/DocumentUploadTests.cs`)

---

## Phase 4: User Story 2 - Browse, Filter & Search (Priority: P1)

**Goal**: Provide document list views, filters, and search for authorized documents.

**Independent Test**: Upload several documents and verify filters, sorting, and search return expected results within performance targets.

- [ ] T020 [P] [US2] Create `MyDocuments` page `ContosoDashboard/Pages/Documents/MyDocuments.razor`
- [ ] T021 [US2] Implement `DocumentSearchService` in `ContosoDashboard/Services/DocumentSearchService.cs` (search by title, tags, description)
- [ ] T022 [US2] Create `DocumentList` component `ContosoDashboard/Shared/Components/DocumentList.razor`
- [ ] T023 [US2] Add integration tests for search/filter functionality (`tests/integration/DocumentSearchTests.cs`)

---

## Phase 5: User Story 3 - Share & Notifications (Priority: P2)

**Goal**: Enable owners to share documents with users/teams and notify recipients.

**Independent Test**: Share document with user; verify recipient receives notification and can access document if authorized.

- [ ] T024 [P] [US3] Implement `DocumentShareService` in `ContosoDashboard/Services/DocumentShareService.cs`
- [ ] T025 [US3] Create share UI `ContosoDashboard/Shared/Components/DocumentShare.razor` and `ContosoDashboard/Pages/Documents/Share.razor`
- [ ] T026 [US3] Integrate sharing with `NotificationService` to send in-app notifications (`ContosoDashboard/Services/NotificationService.cs`)
- [ ] T027 [US3] Add integration tests for share & notification (`tests/integration/DocumentShareTests.cs`)

---

## Phase N: Polish & Cross-Cutting Concerns

- [ ] T028 [P] Documentation updates: quickstart, README, API docs (`specs/main/quickstart.md`, `README.md`)
- [ ] T029 Code cleanup and refactoring (`ContosoDashboard/Services/*`, `ContosoDashboard/Pages/Documents/*`)
- [ ] T030 [P] Add additional unit tests and component tests (`tests/unit/`, `tests/components/`)
- [ ] T031 Security hardening and review (scan config, input sanitization) (`ContosoDashboard/Services/DocumentService.cs`)

---

## Dependencies & Execution Order

- **Setup (Phase 1)**: T001-T004 — prepare CI, config, storage defaults
- **Foundational (Phase 2)**: T005-T013 — blocks all user stories
- **User Stories (Phase 3+)**: T014-T027 — depend on Foundational completion; stories can proceed in parallel
- **Polish (Final Phase)**: T028-T031 — cross-cutting changes after stories

## User Story Dependencies

- **US1 (P1)**: Depends on Foundational (T005-T013)
- **US2 (P1)**: Depends on Foundational and US1 data model but can be built in parallel after foundation
- **US3 (P2)**: Depends on DocumentShare model and Notification service (T006, T013)

## Parallel Opportunities

- Model implementations (T005, T006), storage interface (T007), and local storage implementation (T008) can proceed in parallel.
- UI components for different stories can be implemented in parallel (T014, T020, T025).
- Tests for independent modules can be authored in parallel (T013, T019, T023, T027).

## Implementation Strategy

- MVP: Focus on **User Story 1 (Upload Document)**. Complete Phase 1 + Phase 2 and T014-T019 to deliver a working upload and verification flow.
- Incremental: After MVP, deliver User Story 2 and then User Story 3 in priority order, keeping each story independently testable.

## Checklist Validation

- All tasks follow the required checklist format with IDs, [P] markers for parallelizable tasks, and `[USx]` labels for user story tasks. File paths are included for each implementation task.
