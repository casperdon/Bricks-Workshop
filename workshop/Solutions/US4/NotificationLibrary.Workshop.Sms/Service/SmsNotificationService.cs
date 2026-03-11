using NotificationLibrary.Workshop.Service;
using NotificationLibrary.Workshop.Sms.Mappers;
using NotificationLibrary.Workshop.Sms.Models;

namespace NotificationLibrary.Workshop.Sms.Service;

internal class SmsNotificationService(INotificationService notificationService) : ISmsNotificationService
{
    public async Task<SmsNotificationResponse> SendAsync(TelephoneNumber recipientPhoneNumber, string message, CancellationToken cancellationToken = default)
    {
        recipientPhoneNumber.AssertValid();

        var response = await notificationService.SendAsync(recipientPhoneNumber.Number, "sms", subject: string.Empty, body: message, cancellationToken);

        return SmsNotificationResponseMapper.Map(response);
    }

    public async Task<SmsNotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default)
    {
        var response = await notificationService.GetAsync(notificationId, cancellationToken);

        return response is not null ? SmsNotificationResponseMapper.Map(response) : null;
    }

    public async Task<IReadOnlyList<SmsNotificationResponse>> ListAsync(TelephoneNumber? recipientPhoneNumber = null, string? status = null, CancellationToken cancellationToken = default)
    {
        recipientPhoneNumber?.AssertValid();

        var response = await notificationService.ListAsync(recipientId: recipientPhoneNumber?.Number, channel: "sms", status, cancellationToken);

        return response.Select(SmsNotificationResponseMapper.Map).ToList();
    }
}
