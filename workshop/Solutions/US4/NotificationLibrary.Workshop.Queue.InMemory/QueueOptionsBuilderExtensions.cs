using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Queue.InMemory.Queue;
using NotificationLibrary.Workshop.Queue.InMemory.Worker;
using NotificationLibrary.Workshop.Queue.Services;

namespace NotificationLibrary.Workshop.Queue.InMemory;

public static class QueueOptionsBuilderExtensions
{
    public static QueueOptionsBuilder UseInMemoryQueue(this QueueOptionsBuilder builder)
    {
        builder.Services.AddSingleton<InMemoryMessageQueue>();
        builder.Services.AddSingleton<IMessageQueueReceiver>(sp => sp.GetRequiredService<InMemoryMessageQueue>());
        builder.Services.AddHostedService<MessageQueueWorker>();

        builder.UseQueue<InMemoryMessageQueue>(services =>
            services.AddSingleton<IMessageBus>(sp => sp.GetRequiredService<InMemoryMessageQueue>()));

        return builder;
    }
}
