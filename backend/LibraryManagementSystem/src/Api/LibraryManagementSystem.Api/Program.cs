using Hangfire;
using LibraryManagementSystem.Api.DependencyInjection;
using LibraryManagementSystem.Application.Jobs;
using LibraryManagementSystem.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy =>
        {
            var allowedOrigin = builder.Configuration["Cors:AllowedOrigin"]
                ?? throw new InvalidOperationException("Cors:AllowedOrigin is not configured");
            policy.WithOrigins(allowedOrigin)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

await app.MigrateDatabaseAsync();

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    recurringJobManager.AddOrUpdate<RentalDueSoonReminderJob>(
        "rental-due-soon-reminder",
        job => job.ExecuteAsync(CancellationToken.None),
        Cron.Daily(8));
}

app.UseExceptionHandler();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseCors("AllowAngularClient");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();