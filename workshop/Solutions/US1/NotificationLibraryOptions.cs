using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Options;

namespace NotificationLibrary.Workshop;

public class NotificationLibraryOptions
{
    internal ServiceLifetime NotificationServiceLifeTime { get; set; } = ServiceLifetime.Scoped;
    internal ApiOptions? ApiOptions { get; set; }
    internal string? ApiOptionsConfigurationKey { get; set; }
}
