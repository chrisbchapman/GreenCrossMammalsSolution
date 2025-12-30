using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GreenCross.Mammals.Data;

/// <summary>
/// Design-time factory for EF Core migrations.
/// This is used by dotnet ef commands when the application doesn't use Generic Host.
/// </summary>
public class MammalDbContextFactory : IDesignTimeDbContextFactory<MammalDbContext>
{
    public MammalDbContext CreateDbContext(string[] args)
    {
        // Build configuration from the UI project's appsettings.json
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "GreenCross.Mammals.UI");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MammalDbContext>();

        var connectionString = configuration.GetConnectionString("AppDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'AppDb' not found in appsettings.json. " +
                $"Searched in: {basePath}");
        }

        optionsBuilder.UseSqlServer(
            connectionString,
            sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(30);
                sqlOptions.MaxBatchSize(100);
            });

        return new MammalDbContext(optionsBuilder.Options);
    }
}
