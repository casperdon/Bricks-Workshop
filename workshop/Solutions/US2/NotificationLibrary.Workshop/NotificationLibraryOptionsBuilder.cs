using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Options;

namespace NotificationLibrary.Workshop;

public class NotificationLibraryOptionsBuilder(IServiceCollection services)
{
    public IServiceCollection Services { get; } = services;
    internal NotificationLibraryOptions Options { get; } = new NotificationLibraryOptions();

    public NotificationLibraryOptionsBuilder WithNotificationServiceLifetime(ServiceLifetime lifetime)
    {
        Options.NotificationServiceLifeTime = lifetime;
        return this;
    }

    public NotificationLibraryOptionsBuilder ConfigureApiAccess(string baseUrl, string apiKey)
    {
        Options.ApiOptions = new ApiOptions(baseUrl, apiKey);
        return this;
    }

    public NotificationLibraryOptionsBuilder ConfigureApiAccess(string configurationKey)
    {
        Options.ApiOptionsConfigurationKey = configurationKey;
        return this;
    }

    public NotificationLibraryOptions Build() => Options;
}
