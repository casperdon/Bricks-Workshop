using NotificationService.Client.Responses;

namespace NotificationLibrary.Workshop.Service;

/// <summary>
/// The main abstraction for sending notifications.
///
/// TODO (your task): Design this interface.
///
/// Questions to consider:
///   - What method(s) should this have?
///   - Should callers need to know about channels (email/in-app/push)?
///   - What should the return type be? void? a result object? the notification ID?
///   - Should this be async?
///
/// Think about ALL five consumer teams before you commit to a shape.
/// Look at ConsumerCards.md in the workshop folder.
/// </summary>
public interface INotificationService
{
    // TODO: Add your method(s) here.
    Task<NotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NotificationResponse>> ListAsync(string? recipientId = null, string? channel = null, string? status = null, CancellationToken cancellationToken = default);
    Task<NotificationResponse> SendAsync(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken = default);
}
