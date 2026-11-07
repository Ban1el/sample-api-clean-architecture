using Microsoft.Extensions.DependencyInjection;
using SampleApi.Application.Interfaces;
using SampleApi.Application.Services;

namespace SampleApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}