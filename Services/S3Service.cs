using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

public class S3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public S3Service(IConfiguration config)
    {
        var awsSection = config.GetSection("AWS");
        var credentials = new BasicAWSCredentials(
            awsSection["AccessKey"],
            awsSection["SecretKey"]
        );
        _s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.GetBySystemName(awsSection["Region"]));
        _bucketName = awsSection["BucketName"];
    }
    public async Task<List<string>> ListFiles()
    {
        var request = new ListObjectsV2Request {BucketName = _bucketName};
        var response = await _s3Client.ListObjectsV2Async(request);
        var result = response.S3Objects.Select(o => o.Key).ToList();
        return result;
    }

    public async Task UploadFileAsync(string key, Stream stream)
    {
        var request = new PutObjectRequest{
            BucketName = _bucketName,
            Key = key,
            InputStream = stream
        };
        await _s3Client.PutObjectAsync(request);
    }

    public async Task<Stream> DownloadFile(string key)
    {
        var response = await _s3Client.GetObjectAsync(_bucketName, key);
        return response.ResponseStream;
    }

    public async Task DeleteFileAsync(string key)
    {
        await _s3Client.DeleteObjectAsync(_bucketName, key);
    }
}