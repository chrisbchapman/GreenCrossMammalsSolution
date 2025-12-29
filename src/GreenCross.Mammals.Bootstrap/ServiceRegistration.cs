using GreenCross.Mammals.BLL;
using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Data;
using GreenCross.Mammals.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenCross.Mammals.Bootstrap;

public static class ServiceRegistration
{
    public static IServiceCollection AddGreenCrossApp(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Data / EF
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("AppDb"),
                sqlOptions =>
                {
                    // Enable connection resiliency
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);

                    // Command timeout
                    sqlOptions.CommandTimeout(30);

                    // Batch multiple commands for better performance
                    sqlOptions.MaxBatchSize(100);
                });

            // Query optimization
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            // Development-only settings
#if DEBUG
            options.EnableSensitiveDataLogging(configuration.GetValue<bool>("EnableSensitiveDataLogging", false));
            options.EnableDetailedErrors(configuration.GetValue<bool>("EnableDetailedErrors", false));
#endif

        }, ServiceLifetime.Scoped);

        // Use pooling for better performance
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("AppDb"),
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                    sqlOptions.CommandTimeout(30);
                    sqlOptions.MaxBatchSize(100);
                });

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

#if DEBUG
            options.EnableSensitiveDataLogging(configuration.GetValue<bool>("EnableSensitiveDataLogging", false));
            options.EnableDetailedErrors(configuration.GetValue<bool>("EnableDetailedErrors", false));
#endif
        });

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories (Data implements Contracts)
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRecorderRepository, RecorderRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IValidationStatusRepository, ValidationStatusRepository>();
        services.AddScoped<IHarvestMouseRecordRepository, HarvestMouseRecordRepository>();

        // BLL services
        services.AddScoped<IRecorderService, RecorderService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IValidationStatusService, ValidationStatusService>();
        services.AddScoped<IHarvestMouseRecordService, HarvestMouseRecordService>();

        return services;
    }
}
