using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Options;
using NotificationLibrary.Workshop.Service;
using NotificationService.Client;

namespace NotificationLibrary.Workshop;

public static class ServiceCollectionExtensions
{
    const string HttpClientName = "NotificationLibrary";

    public static void AddNotifications(this IServiceCollection services, IConfiguration configuration, Action<NotificationLibraryOptionsBuilder> configureOptions)
    {
        var builder = new NotificationLibraryOptionsBuilder();

        configureOptions(builder);

        var options = builder.Build();

        if (options.ApiOptionsConfigurationKey == null && options.ApiOptions == null)
        {
            throw new InvalidOperationException("Api Configuration must be provided either through direct configuration or by specifying a configuration key.");
        }

        var apiOptions = options.ApiOptions ?? configuration.GetRequiredSection(options.ApiOptionsConfigurationKey!).Get<ApiOptions>()!;

        services.Add(new ServiceDescriptor(typeof(INotificationService), typeof(Service.NotificationService), options.NotificationServiceLifeTime));

        services.AddHttpClient(HttpClientName, client =>
        {
            client.BaseAddress = new Uri(apiOptions.BaseUrl);
        });

        services.AddScoped<NotificationApiClient>(sp => new(sp.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientName), apiOptions.ApiKey));
    }
}
