using Microsoft.AspNetCore.Mvc;
using UserApi.Services.Files;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{fileId}")]
    public async Task<IActionResult> GetFile(long fileId)
    {
        var file = await _fileService.GetFileAsync(fileId);
        var imageFileStream = System.IO.File.OpenRead(file.FilePath);
        return File(imageFileStream, file.FileContentType, file.FileName);
    }

    [HttpPost("upload-file")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }
        long fileSizeInKB = file.Length / 1024;

        if (fileSizeInKB > 100)
        {
            return BadRequest("Файл превышает 100 КБ.");
        }

        var fileId = await _fileService.UploadFileAsync(file);
        return Ok(fileId);
    }
    [HttpDelete("remove-file/{fileId}")]
    public async Task<IActionResult> RemoveFile(long fileId)
    {
        await _fileService.RemoveFileAsync(fileId);
        return Ok();
    }

}
