using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Queue.Services;

namespace NotificationLibrary.Workshop.Queue;

public static class NotificationLibraryOptionsBuilderExtensions
{
    public static NotificationLibraryOptionsBuilder AddMessageBus(this NotificationLibraryOptionsBuilder builder, Action<QueueOptionsBuilder> configure)
    {
        var builderOptions = new QueueOptionsBuilder(builder.Services);

        configure(builderOptions);

        var options = builderOptions.Build();

        if (options.MessageBusImplementationType == null && options.MessageBusRegistration == null)
        {
            throw new InvalidOperationException("Message bus implementation type must be specified.");
        }

        builder.Services.AddScoped<IDistributedNotificationService, DistributedNotificationService>();

        if (options.MessageBusRegistration != null)
            options.MessageBusRegistration(builder.Services);
        else
            builder.Services.AddScoped(typeof(IMessageBus), options.MessageBusImplementationType!);

        return builder;
    }
}