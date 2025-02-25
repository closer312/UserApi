using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserApi.Constants;
using UserApi.Data;
using UserApi.Dtos.Files;
using UserApi.Models.Files;

namespace UserApi.Services.Files.Impl;

public class FileService : IFileService
{
    private readonly AppPostgreSqlDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMapper _mapper;
    public FileService(AppPostgreSqlDbContext context, IWebHostEnvironment webHostEnvironment, IMapper mapper)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _mapper = mapper;
    }

    public async Task<FileResponse> GetFileAsync(long fileId)
    {
        var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId);
        if (file == null)
            throw new FileNotFoundException("Файл не найден в базе данных.");

        return _mapper.Map<FileResponse>(file);
    }
    public async Task RemoveFileAsync(long fileId)
    {
        var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId);
        if (file == null)
            throw new FileNotFoundException("Файл не найден в базе данных.");

        string filePath = file.Path;

        if (File.Exists(filePath))
            File.Delete(filePath);

        _context.Files.Remove(file);
        await _context.SaveChangesAsync();
    }

    public async Task<long> UploadFileAsync(IFormFile file)
    {
        var appFile = await CreateAppFile(file);
        await _context.Files.AddAsync(appFile);
        await _context.SaveChangesAsync();
        return appFile.Id;
    }

    private async Task<AppFile> CreateAppFile(IFormFile file)
    {
        var appFile = new AppFile
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            Path = await SaveFileAsync(file)
        };
        return appFile;
    }

    private async Task<string> SaveFileAsync(IFormFile file)
    {
        string fileName = $"{Guid.NewGuid()}_{file.FileName}";

        string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, FilePathsConstants.FolderPath);

        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);


        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return filePath;
    }
}
