namespace NotificationService.Api.Models;

public record SendNotificationRequest(
    string? RecipientId,
    string? Channel,
    string? Subject,
    string? Body);