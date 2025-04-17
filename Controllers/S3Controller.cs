using Microsoft.AspNetCore.Mvc;


namespace AWSService.Controllers;
[ApiController]
[Route("api/[controller]")]
public class S3Controller : ControllerBase{
    private readonly S3Service _s3Service;

    public S3Controller(S3Service s3Service)
    {
        _s3Service = s3Service;
    }

    [HttpGet("ListFiles")]
    public async Task<IActionResult> ListFiles(){
        var files = await _s3Service.ListFiles();
        return Ok(files);
    }

    // [HttpPost("UploadFile")]
    // public async Task<IActionResult> UploadFile([FromForm] IFormFile file){

    //     if(file == null || file.Length == 0){
    //         return BadRequest("File is required");
    //     }

    //     using var stream = file.OpenReadStream();
    //     await _s3Service.UploadFileAsync(file.FileName, stream);
    //     return Ok(new {message = "File uploaded successfully"});
    // }

    [HttpGet("DownloadFiles")]
    public async Task<IActionResult> DownloadFiles(string key){
        var stream = await _s3Service.DownloadFile(key);
        return File(stream, "application/octet-stream", key);
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(string key)
    {
        await _s3Service.DeleteFileAsync(key);
        return Ok(new { message = "Deleted successfully" });
    }
}