using NotificationService.Client.Responses;

namespace NotificationLibrary.Workshop.Service;

public interface INotificationService
{
    Task<NotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NotificationResponse>> ListAsync(string? recipientId = null, string? channel = null, string? status = null, CancellationToken cancellationToken = default);
    Task<NotificationResponse> SendAsync(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken = default);
}
