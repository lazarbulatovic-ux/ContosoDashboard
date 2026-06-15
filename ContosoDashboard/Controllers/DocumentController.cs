using ContosoDashboard.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContosoDashboard.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string title, [FromForm] string category, [FromForm] int? projectId, [FromForm] string? tags)
        {
            if (file == null || file.Length == 0) return BadRequest("File is required");

            var userId = 1; // TODO: extract from authenticated user claims

            using var stream = file.OpenReadStream();
            var doc = await _documentService.CreateAsync(stream, file.FileName, title, category, userId, projectId, tags, file.ContentType);
            return Ok(new { doc.DocumentId, doc.FilePath, doc.ScanStatus });
        }
    }
}
