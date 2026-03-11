using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationLibrary.Workshop.Queue.RabbitMQ.Connection;
using NotificationLibrary.Workshop.Service;
using NotificationService.Client.Requests;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationLibrary.Workshop.Queue.RabbitMQ.Worker;

internal sealed class RabbitMQConsumerWorker(
    RabbitMQConnectionProvider connectionProvider,
    RabbitMQOptions options,
    IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = await connectionProvider.GetConnectionAsync(stoppingToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var message = JsonSerializer.Deserialize<SendNotificationRequest>(json);

            if (message != null)
            {
                using var scope = serviceProvider.CreateScope();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                await notificationService.SendAsync(
                    message.RecipientId,
                    message.Channel,
                    message.Subject ?? string.Empty,
                    message.Body,
                    stoppingToken);
            }

            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
        };

        await channel.BasicConsumeAsync(options.QueueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException) { }
    }
}
