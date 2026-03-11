
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationLibrary.Workshop.Queue.Services;
using NotificationService.Client.Requests;
using NotificationLibrary.Workshop.Service;

namespace NotificationLibrary.Workshop.Queue.InMemory.Worker;

internal class MessageQueueWorker(
    IMessageQueueReceiver messageQueueReceiver,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            SendNotificationRequest? message;
            try
            {
                message = await messageQueueReceiver.TryDequeueAsync(stoppingToken);
            }
            catch (OperationCanceledException) { break; }

            if (message != null)
            {
                using var scope = serviceProvider.CreateScope();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                await notificationService.SendAsync(message.RecipientId, message.Channel, message.Subject ?? string.Empty, message.Body, stoppingToken);
            }
        }
    }
}

