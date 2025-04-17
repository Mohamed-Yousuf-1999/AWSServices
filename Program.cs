using Amazon;
using Amazon.Runtime;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

//Get AWS Section from config
var awsConfig = builder.Configuration.GetSection("AWS");
var accessKey = awsConfig["AccessKey"];
var secretKey = awsConfig["SecretKey"];
var region = awsConfig["Region"];

//Create AWS credentials manually
var credentials = new BasicAWSCredentials(accessKey, secretKey);
var regionEndPoint = RegionEndpoint.GetBySystemName(region);

//Register AWS S3 with manual config
builder.Services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(credentials, regionEndPoint));

//Custom S3 Service
builder.Services.AddScoped<S3Service>();

// // Add services to the container.
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new() { Title = "AWSService", Version = "v1" });

//     // 🛠️ Add this block to handle file upload params
//     c.OperationFilter<FileUploadOperationFilter>();
// });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
