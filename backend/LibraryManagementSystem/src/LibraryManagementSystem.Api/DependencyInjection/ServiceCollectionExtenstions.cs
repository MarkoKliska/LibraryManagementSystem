using LibraryManagementSystem.Infrastructure.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LibraryManagementSystem.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        // Dodaj kontrolere
        services.AddControllers();

        // Dodaj Application sloj
        //services.AddApplicationServices();

        // Dodaj Infrastructure sloj
        services.AddInfrastructure(config);

        // Swagger konfiguracija
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
