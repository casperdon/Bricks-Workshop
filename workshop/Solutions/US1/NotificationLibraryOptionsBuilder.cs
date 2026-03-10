using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Options;

namespace NotificationLibrary.Workshop;

public class NotificationLibraryOptionsBuilder
{
    private readonly NotificationLibraryOptions _options = new NotificationLibraryOptions();

    public NotificationLibraryOptionsBuilder WithNotificationServiceLifetime(ServiceLifetime lifetime)
    {
        _options.NotificationServiceLifeTime = lifetime;
        return this;
    }

    public NotificationLibraryOptionsBuilder ConfigureApiAccess(string baseUrl, string apiKey)
    {
        _options.ApiOptions = new ApiOptions(baseUrl, apiKey);
        return this;
    }

    public NotificationLibraryOptionsBuilder ConfigureApiAccess(string configurationKey)
    {
        _options.ApiOptionsConfigurationKey = configurationKey;
        return this;
    }

    public NotificationLibraryOptions Build() => _options;
}
