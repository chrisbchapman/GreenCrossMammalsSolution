using GreenCross.Mammals.Bootstrap;
using GreenCross.Mammals.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        // Setup DI
        var services = new ServiceCollection();

        // Add optimized logging
        services.AddLogging(builder =>
        {
            builder.AddConfiguration(configuration.GetSection("Logging"));
            builder.AddConsole(options =>
            {
                options.FormatterName = "simple";
            });

#if DEBUG
            builder.AddDebug();
#endif
            // Apply filtering rules from configuration
            builder.SetMinimumLevel(LogLevel.Information);
        });

        // Register configuration
        services.AddSingleton<IConfiguration>(configuration);

        // Bootstrap app services
        services.AddGreenCrossApp(configuration);

        // UI registrations
        services.AddWinFormsUi();

        // Build and run
        using var serviceProvider = services.BuildServiceProvider();

        var logger = serviceProvider.GetRequiredService<ILogger<MainForm>>();

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Application starting...");
        }

        try
        {
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Application failed to start");
            throw;
        }
    }
}
