using NotificationService.Api.Models;
using NotificationService.Api.Store;

namespace NotificationService.Api.Endpoints;

public static class NotificationEndpoints
{
    public static void MapNotificationEndpoints(this WebApplication app)
    {
        app.MapPost("/notifications", async (
        SendNotificationRequest request,
        INotificationStore store,
        IConfiguration config) =>
        {
            string[] validChannels = ["email", "in-app", "push"];

            if (!validChannels.Contains(request.Channel?.ToLowerInvariant()))
                return Results.BadRequest(new { error = $"Invalid channel. Supported values: {string.Join(", ", validChannels)}" });

            if (string.IsNullOrWhiteSpace(request.RecipientId))
                return Results.BadRequest(new { error = "RecipientId is required." });

            if (string.IsNullOrWhiteSpace(request.Body))
                return Results.BadRequest(new { error = "Body is required." });

            var failureRate = config.GetValue<int>("NotificationApi:FailureRatePercent", 15);
            var delayMs = config.GetValue<int>("NotificationApi:ProcessingDelayMs", 80);

            await Task.Delay(delayMs);

            var status = Random.Shared.Next(100) < failureRate
                ? NotificationStatus.Failed
                : NotificationStatus.Sent;

            var notification = new NotificationRecord(
                Id: Guid.NewGuid().ToString("N"),
                RecipientId: request.RecipientId,
                Channel: request.Channel!.ToLowerInvariant(),
                Subject: request.Subject ?? string.Empty,
                Body: request.Body,
                Status: status,
                CreatedAt: DateTimeOffset.UtcNow);

            store.Add(notification);

            return Results.Created($"/notifications/{notification.Id}", notification.ToResponse());
        })
        .WithName("SendNotification")
        .WithSummary("Send a notification")
        .WithDescription(
            "Sends a notification to the specified recipient via the chosen channel. " +
            "Simulates a configurable failure rate (default 15%) to make resilience scenarios realistic.");

        app.MapGet("/notifications/{id}", (string id, INotificationStore store) =>
            store.TryGet(id, out var n)
                ? Results.Ok(n!.ToResponse())
                : Results.NotFound(new { error = $"Notification with '{id}' not found." }))
        .WithName("GetNotification")
        .WithSummary("Get a notification by ID");

        app.MapGet("/notifications", (
            string? recipientId,
            string? channel,
            string? status,
            INotificationStore store) =>
        {
            var results = store.GetAll(recipientId, channel, status);
            return Results.Ok(results.Select(n => n.ToResponse()));
        })
        .WithName("ListNotifications")
        .WithSummary("List notifications")
        .WithDescription("Returns all notifications, optionally filtered by recipientId, channel, and/or status.");
    }
}
