using AWSService.Interfaces.IRepository;
using AWSService.Repositories;

namespace AWSService.StarupDI
{
    public static class RepositoriesExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IRdsRepository, RdsRepository>();
            return repositories;
        }
    }
}

