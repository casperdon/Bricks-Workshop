using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationLibrary.Workshop.Queue.Services;

public interface IDistributedNotificationService
{
    Task EnqueueAsync(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken = default);
}
