using NotificationService.Client;
using NotificationService.Client.Requests;
using NotificationService.Client.Responses;

namespace NotificationLibrary.Workshop.Service;

internal class NotificationService(NotificationApiClient client) : INotificationService
{
    public async Task<NotificationResponse> SendAsync(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken = default)
    {
        var response = await client.SendAsync(new SendNotificationRequest
        {
            RecipientId = recipientId,
            Channel = channel,
            Subject = subject,
            Body = body
        }, cancellationToken);

        return response;
    }

    public Task<NotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default) 
        => client.GetAsync(notificationId, cancellationToken);

    public Task<IReadOnlyList<NotificationResponse>> ListAsync(string? recipientId = null, string? channel = null, string? status = null, CancellationToken cancellationToken = default) 
        => client.ListAsync(recipientId, channel, status, cancellationToken);
}
