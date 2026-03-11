using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Queue.Services;

namespace NotificationLibrary.Workshop.Queue;

public class QueueOptionsBuilder(IServiceCollection services)
{
    public IServiceCollection Services { get; } = services;
    private readonly QueueOptions _options = new QueueOptions();

    internal void UseQueue<T>() where T : IMessageBus
    {
        _options.MessageBusImplementationType = typeof(T);
    }

    internal void UseQueue<T>(Action<IServiceCollection> registration) where T : IMessageBus
    {
        _options.MessageBusImplementationType = typeof(T);
        _options.MessageBusRegistration = registration;
    }

    public QueueOptions Build() => _options;
}
