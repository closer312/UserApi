using UserApi.Dtos.Files;

namespace UserApi.Services.Files;

public interface IFileService
{
    Task<long> UploadFileAsync(IFormFile file);
    Task RemoveFileAsync(long fileId);
    Task<FileResponse> GetFileAsync(long fileId);
}
