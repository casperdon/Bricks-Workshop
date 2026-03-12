using Microsoft.Extensions.DependencyInjection;

namespace NotificationLibrary.Workshop;

/// <summary>
/// Extension methods for registering the notification library with the DI container.
///
/// TODO (your task): Implement the registration method(s) here.
///
/// User Story 1's requirement is: "ONE line in Program.cs — sensible defaults."
/// That means something like:
///
///     builder.Services.AddNotifications(options =>
///     {
///         options.ApiKey  = "...";
///         options.BaseUrl = "https://...";
///     });
///
/// Questions to consider:
///   - What lifetime should INotificationService have? (Singleton? Scoped? Transient?)
///   - How do you register HttpClient cleanly? (Hint: IHttpClientFactory / AddHttpClient)
///   - Should there be an overload that accepts IConfiguration directly?
/// </summary>
public static class ServiceCollectionExtensions
{
    // TODO: Add your AddNotifications(...) extension method here.
}
