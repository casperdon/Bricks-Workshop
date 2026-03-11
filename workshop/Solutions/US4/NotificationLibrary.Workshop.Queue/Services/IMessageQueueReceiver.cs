using NotificationService.Client.Requests;

namespace NotificationLibrary.Workshop.Queue.Services;

internal interface IMessageQueueReceiver
{
    Task<SendNotificationRequest?> TryDequeueAsync(CancellationToken cancellationToken);
}
