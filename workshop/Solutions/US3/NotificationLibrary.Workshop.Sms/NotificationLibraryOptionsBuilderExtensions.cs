using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Sms.Service;

namespace NotificationLibrary.Workshop.Sms;

public static class NotificationLibraryOptionsBuilderExtensions
{
    public static NotificationLibraryOptionsBuilder AddSms(this NotificationLibraryOptionsBuilder builder)
    {
        builder.Services.Add(new ServiceDescriptor(typeof(ISmsNotificationService), typeof(SmsNotificationService), builder.Options.NotificationServiceLifeTime));

        return builder;
    }
}