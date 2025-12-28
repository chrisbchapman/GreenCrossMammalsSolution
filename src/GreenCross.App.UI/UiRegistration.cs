// GreenCross.<App>.UI
using GreenCross.App.UI;
using Microsoft.Extensions.DependencyInjection;

public static class UiRegistration
{
    public static IServiceCollection AddWinFormsUi(this IServiceCollection services)
    {
        services.AddTransient<MainForm>();
        return services;
    }
}
