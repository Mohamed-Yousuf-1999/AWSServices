using AWSService.Interfaces.IServices;
using AWSService.Services;

namespace AWSService.StarupDI;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Register your application services here
        services.AddScoped<IRdsService, RdsService>();
        services.AddScoped<IS3Service, S3Service>();

        return services;
    }
}
