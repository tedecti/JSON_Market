using JSON_Market.Repository.Interfaces;

namespace JSON_Market.Repository;

public class FileRepository : IFileRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _localDirectory;

    public FileRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _localDirectory = _configuration["LocalStorage:Path"] ?? "Images";
        if (!Directory.Exists(_localDirectory))
        {
            Directory.CreateDirectory(_localDirectory);
        }
    }

    public async Task<string> SaveFile(IFormFile file)
    {
        var myUuid = Guid.NewGuid();
        var fileName = $"{myUuid}.{file.ContentType.Split("/")[1]}";
        var filePath = Path.Combine(_localDirectory, fileName);

        try
        {
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Ошибка при загрузке изображения");
        }
    }

    public async Task<Stream> GetFile(string fileName)
    {
        var filePath = Path.Combine(_localDirectory, fileName);

        if (!File.Exists(filePath))
            return null;

        try
        {
            var memoryStream = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memoryStream);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Ошибка при получении изображения");
            return null;
        }
    }

    public async Task<bool> DeleteFileFromStorage(string fileName)
    {
        var filePath = Path.Combine(_localDirectory, fileName);

        if (!File.Exists(filePath))
            return false;

        try
        {
            File.Delete(filePath);
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException("Ошибка при удалении изображения");
            return false;
        }
    }
}