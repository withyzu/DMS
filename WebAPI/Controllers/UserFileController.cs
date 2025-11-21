using Microsoft.Extensions.Options;

namespace FileAssistant.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserFileController : ControllerBase
{
    private readonly UserFileOptions _pdfSettings;

    public UserFileController(IOptions<UserFileOptions> pdfSettings)
    {
        _pdfSettings = pdfSettings.Value;
    }

    /// <summary>
    /// Find all PDF files in the storage directory.
    /// </summary>
    /// <returns></returns>
    [HttpGet("find-all")]
    public ActionResult<List<string>> FindAll()
    {
        try
        {
            if (!Directory.Exists(_pdfSettings.ResolvedStoragePath))
            {
                return NotFound("PDF storage directory does not exist.");
            }

            var pdfFiles = Directory.GetFiles(_pdfSettings.ResolvedStoragePath)
            .Select(Path.GetFileName)
            .ToList();

            return Ok(pdfFiles);
        }

        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
