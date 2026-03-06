namespace NotificationService.Api.Models;

public record NotificationResponse(
    string Id,
    string RecipientId,
    string Channel,
    string Subject,
    string Body,
    string Status,
    DateTimeOffset CreatedAt);
