using Microsoft.Extensions.DependencyInjection;

namespace NotificationLibrary.Workshop;

/// <summary>
/// Extension methods for registering the notification library with the DI container.
///
/// TODO (your task): Implement the registration method(s) here.
///
/// Team A's requirement is: "ONE line in Program.cs — sensible defaults."
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
///   - Team D needs to bring their own ILogger. Does your registration force a logger on them,
///     or does it pick up whatever is already registered?
///   - Should there be an overload that accepts IConfiguration directly?
/// </summary>
public static class ServiceCollectionExtensions
{
    // TODO: Add your AddNotifications(...) extension method here.
}
