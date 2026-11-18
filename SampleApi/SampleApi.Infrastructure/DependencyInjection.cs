using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleApi.Application.Interfaces;
using SampleApi.Infrastructure.Configurations;
using SampleApi.Infrastructure.Repositories;

namespace SampleApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.Configure<DatabaseOptions>(
         configuration.GetSection(DatabaseOptions.SectionName)
     );

        return services;
    }
}