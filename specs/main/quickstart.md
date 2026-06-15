# Quickstart: Document Upload and Management (Local)

1. Build and run the application (from repository root):

```powershell
dotnet build
dotnet run --project ContosoDashboard\ContosoDashboard.csproj
```

2. Ensure local storage directory exists (example):

```powershell
mkdir $env:LOCALAPPDATA\ContosoDashboard\uploads -Force
```

3. Configuration (appsettings.json)
- `FileStorage:Type` = `Local`
- `FileStorage:Local:BasePath` = `%LOCALAPPDATA%\ContosoDashboard\uploads`

4. Run basic upload scenario via UI: open browser at the running app, login,
   navigate to Documents and upload a supported file under 25 MB.
