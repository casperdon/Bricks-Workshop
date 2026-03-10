using NotificationLibrary.Workshop.Email.Models;

namespace NotificationLibrary.Workshop.Email.Service;

public interface IEmailNotificationService
{
    Task<EmailNotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmailNotificationResponse>> ListAsync(Models.Email? recipientAdress = null, string? status = null, CancellationToken cancellationToken = default);
    Task<EmailNotificationResponse> SendAsync(Models.Email recipientAdress, string subject, string body, CancellationToken cancellationToken = default);
}
