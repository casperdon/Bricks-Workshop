using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Email.Service;

namespace NotificationLibrary.Workshop.Email;

public static class NotificationLibraryOptionsBuilderExtensions
{
    public static NotificationLibraryOptionsBuilder AddEmail(this NotificationLibraryOptionsBuilder builder)
    {
        builder.Services.Add(new ServiceDescriptor(typeof(IEmailNotificationService), typeof(EmailNotificationService), builder.Options.NotificationServiceLifeTime));

        return builder;
    }
}