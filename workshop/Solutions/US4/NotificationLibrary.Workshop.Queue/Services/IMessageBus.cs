using NotificationService.Client.Requests;

namespace NotificationLibrary.Workshop.Queue.Services;

internal interface IMessageBus
{
    public Task PublishAsync(SendNotificationRequest message, CancellationToken cancellationToken = default);
}
