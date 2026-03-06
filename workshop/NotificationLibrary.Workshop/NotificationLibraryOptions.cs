namespace NotificationLibrary.Workshop;

/// <summary>
/// Configuration options for the notification library.
///
/// TODO (your task): Design this class.
///
/// Questions to consider:
///   - What does the library ALWAYS need? (hint: base URL, API key)
///   - What should have sensible defaults vs. require explicit setup?
///   - Should this be flat properties, or a fluent builder pattern?
///   - Should Team E (multi-tenant, different API key per request) change
///     this class, or be handled somewhere else entirely?
///
/// Tip: Look at how ASP.NET Core's own libraries use IOptions&lt;T&gt; for inspiration.
/// </summary>
public class NotificationLibraryOptions
{
    // TODO: Add your options here.
    //
    // You will need at minimum:
    //   - The base URL of the Notification Service API
    //   - An API key
    //
    // You might also want to consider:
    //   - Default channel
    //   - Timeout settings
    //   - Retry policy configuration
}
