using LibraryManagementSystem.Infrastructure.DependencyInjection;
using LibraryManagementSystem.Application.DependencyInjection;

using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddApplicationServices();

        services.AddInfrastructure(config);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Library Management System API",
                Version = "v1"
            });
        });

        return services;
    }
}