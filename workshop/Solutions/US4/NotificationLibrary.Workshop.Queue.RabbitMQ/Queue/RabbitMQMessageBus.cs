using System.Text;
using System.Text.Json;
using NotificationLibrary.Workshop.Queue.RabbitMQ.Connection;
using NotificationLibrary.Workshop.Queue.Services;
using NotificationService.Client.Requests;
using RabbitMQ.Client;

namespace NotificationLibrary.Workshop.Queue.RabbitMQ.Queue;

internal sealed class RabbitMQMessageBus(RabbitMQConnectionProvider connectionProvider, RabbitMQOptions options) : IMessageBus
{
    public async Task PublishAsync(SendNotificationRequest message, CancellationToken cancellationToken = default)
    {
        var connection = await connectionProvider.GetConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            queue: options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        var props = new BasicProperties { Persistent = true };

        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: options.QueueName,
            mandatory: false,
            basicProperties: props,
            body: body,
            cancellationToken: cancellationToken);
    }
}
