using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Queue.RabbitMQ.Connection;
using NotificationLibrary.Workshop.Queue.RabbitMQ.Queue;
using NotificationLibrary.Workshop.Queue.RabbitMQ.Worker;

namespace NotificationLibrary.Workshop.Queue.RabbitMQ;

public static class QueueOptionsBuilderExtensions
{
    public static QueueOptionsBuilder UseRabbitMQQueue(this QueueOptionsBuilder builder, Action<RabbitMQOptionsBuilder> configure)
    {
        var optionsBuilder = new RabbitMQOptionsBuilder();
        configure.Invoke(optionsBuilder);
        var options = optionsBuilder.Build();

        builder.Services.AddSingleton(options);
        builder.Services.AddSingleton<RabbitMQConnectionProvider>();
        builder.Services.AddHostedService<RabbitMQConsumerWorker>();

        builder.UseQueue<RabbitMQMessageBus>();

        return builder;
    }
}
