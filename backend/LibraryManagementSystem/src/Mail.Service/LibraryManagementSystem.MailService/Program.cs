using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.Contracts.User;
using LibraryManagementSystem.MailService.Consumers;
using LibraryManagementSystem.MailService.Emailing;
using LibraryManagementSystem.MailService.Templates;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IEmailSender, MailKitEmailSender>();

builder.Services.AddScoped<IEmailTemplate<UserRegisteredIntegrationEvent>, UserRegisteredEmailTemplate>();
builder.Services.AddScoped<IEmailTemplate<BookRentedIntegrationEvent>, BookRentedEmailTemplate>();
builder.Services.AddScoped<IEmailTemplate<BookReturnedIntegrationEvent>, BookReturnedEmailTemplate>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserRegisteredIntegrationEventConsumer>();
    x.AddConsumer<BookRentedIntegrationEventConsumer>();
    x.AddConsumer<BookReturnedIntegrationEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"] ?? "guest");
            h.Password(builder.Configuration["RabbitMq:Password"] ?? "guest");
        });

        cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();