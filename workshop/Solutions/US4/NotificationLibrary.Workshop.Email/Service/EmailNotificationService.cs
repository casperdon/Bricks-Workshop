using NotificationLibrary.Workshop.Email.Mappers;
using NotificationLibrary.Workshop.Email.Models;
using NotificationLibrary.Workshop.Service;

namespace NotificationLibrary.Workshop.Email.Service;

internal class EmailNotificationService(INotificationService notificationService) : IEmailNotificationService
{
    public async Task<EmailNotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken)
    {
        var response = await notificationService.GetAsync(notificationId, cancellationToken);

        return response is not null ? EmailNotificationResponseMapper.Map(response) : null;
    }

    public async Task<IReadOnlyList<EmailNotificationResponse>> ListAsync(Models.Email? recipientAdress = null, string? status = null, CancellationToken cancellationToken = default)
    {
        recipientAdress?.AssertValid();

        var response = await notificationService.ListAsync(recipientId: recipientAdress?.Address, channel: "email", status, cancellationToken);

        return response.Select(EmailNotificationResponseMapper.Map).ToList();
    }

    public async Task<EmailNotificationResponse> SendAsync(Models.Email recipientAdress, string subject, string body, CancellationToken cancellationToken = default)
    {
        recipientAdress.AssertValid();

        var response = await notificationService.SendAsync(recipientAdress.Address, "email", subject, body, cancellationToken);

        return EmailNotificationResponseMapper.Map(response);
    }
}
