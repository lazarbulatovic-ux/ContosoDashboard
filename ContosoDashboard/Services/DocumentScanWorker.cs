using ContosoDashboard.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoDashboard.Services
{
    public class DocumentScanWorker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<DocumentScanWorker> _logger;

        public DocumentScanWorker(IServiceProvider services, ILogger<DocumentScanWorker> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DocumentScanWorker started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var pending = await db.Documents
                        .Where(d => d.ScanStatus == Models.ScanStatus.Pending)
                        .OrderBy(d => d.CreatedAt)
                        .Take(10)
                        .ToListAsync(stoppingToken);
                    foreach (var doc in pending)
                    {
                        // Simulate scan (MVP): mark Available after a short delay
                        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                        doc.ScanStatus = Models.ScanStatus.Available;
                        doc.ScanMetadata = "MVP-scan:passed";
                        db.Documents.Update(doc);
                    }

                    if (pending.Count > 0)
                    {
                        await db.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Processed {Count} pending documents", pending.Count);
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in DocumentScanWorker");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
