using NotificationService.Client.Requests;

namespace NotificationLibrary.Workshop.Queue.Services;

internal class DistributedNotificationService(IMessageBus messageBus) : IDistributedNotificationService
{
    public async Task EnqueueAsync(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken = default)
    {
        var notification = new SendNotificationRequest
        {
            RecipientId = recipientId,
            Channel = channel,
            Subject = subject,
            Body = body
        };

        await messageBus.PublishAsync(notification, cancellationToken);
    }
}
