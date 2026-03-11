using Microsoft.Extensions.DependencyInjection;

namespace NotificationLibrary.Workshop.Queue;

public class QueueOptions
{
    internal Type? MessageBusImplementationType { get; set; }
    internal Action<IServiceCollection>? MessageBusRegistration { get; set; }
}
