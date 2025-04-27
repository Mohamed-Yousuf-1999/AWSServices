using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using AWSService.Interfaces.IServices;

namespace AWSService.Services;
public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName = string.Empty;

    public S3Service(IConfiguration config)
    {
        var awsSection = config.GetSection("AWS");
        var credentials = new BasicAWSCredentials(
            awsSection["AccessKey"],
            awsSection["SecretKey"]
        );
        _s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.GetBySystemName(awsSection["Region"]));
        _bucketName = awsSection["BucketName"] ?? string.Empty;
    }
    public async Task<List<string>> ListFiles()
    {
        var request = new ListObjectsV2Request {BucketName = _bucketName};
        var response = await _s3Client.ListObjectsV2Async(request);
        var result = response.S3Objects.Select(o => o.Key).ToList();
        return result;
    }

    public async Task<string> GetFilePresignedUrl(string key){
        var request = new GetPreSignedUrlRequest{
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.Now.AddMinutes(1)
        };
        var result = await _s3Client.GetPreSignedURLAsync(request);
        return result;
    }

    public async Task UploadFileAsync(string key, string extension, Stream stream)
    {
        var fileKey = $"{extension}/{key}";
        var request = new PutObjectRequest{
            BucketName = _bucketName,
            Key = fileKey,
            InputStream = stream
        };
        await _s3Client.PutObjectAsync(request);
    }

    public async Task<Stream> DownloadFile(string key)
    {
        var extension = Path.GetExtension(key).TrimStart('.').ToUpperInvariant();

        var response = await _s3Client.GetObjectAsync(_bucketName, $"{extension}/{key}");
        return response.ResponseStream;
    }

    public async Task DeleteFileAsync(string key)
    {
        var extension = Path.GetExtension(key).TrimStart('.').ToUpperInvariant();

        await _s3Client.DeleteObjectAsync(_bucketName, $"{extension}/{key}");
    }
}