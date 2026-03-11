using Microsoft.Extensions.DependencyInjection;
using NotificationLibrary.Workshop.Service;
using NotificationService.Client.Responses;
using Polly;

namespace NotificationLibrary.Workshop.Polly;

internal class PollyNotificationService(
    Service.NotificationService innerService,
    [FromKeyedServices(PollyConstants.ResiliencePipelineName)] ResiliencePipeline pipeline) 
    : INotificationService
{
    public async Task<NotificationResponse?> GetAsync(string notificationId, CancellationToken cancellationToken = default) 
        => await pipeline.ExecuteAsync(async token => await innerService.GetAsync(notificationId, token), cancellationToken);
    public async Task<IReadOnlyList<NotificationResponse>> ListAsync(string? recipientId = null, string? channel = null, string? status = null, CancellationToken cancellationToken = default) 
        => await pipeline.ExecuteAsync(async token => await innerService.ListAsync(recipientId, channel, status, token), cancellationToken);
    public async Task<NotificationResponse> SendAsync(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken = default) 
        => await pipeline.ExecuteAsync(async token => await innerService.SendAsync(recipientId, channel, subject, body, token), cancellationToken);
}
