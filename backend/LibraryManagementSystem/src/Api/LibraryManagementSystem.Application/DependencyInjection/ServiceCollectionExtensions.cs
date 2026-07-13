using LibraryManagementSystem.Application.Jobs;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryManagementSystem.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddScoped<RentalDueSoonReminderJob>();

        return services;
    }
}
