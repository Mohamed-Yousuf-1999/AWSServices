namespace AWSService.Interfaces.IServices;

public interface IS3Service
{
    Task<List<string>> ListFiles();
    Task<string> GetFilePresignedUrl(string key);
    Task UploadFileAsync(string key, string extension, Stream stream);
    Task<Stream> DownloadFile(string key);
    Task DeleteFileAsync(string key);
}