using NotificationLibrary.Workshop.Sms.Models;

namespace NotificationLibrary.Workshop.Sms.Service;

public interface ISmsNotificationService
{
    Task<SmsNotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SmsNotificationResponse>> ListAsync(TelephoneNumber? recipientPhoneNumber = null, string? status = null, CancellationToken cancellationToken = default);
    Task<SmsNotificationResponse> SendAsync(TelephoneNumber recipientPhoneNumber, string message, CancellationToken cancellationToken = default);
}
