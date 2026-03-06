namespace NotificationService.Api.Models;

public record NotificationRecord(
    string Id,
    string RecipientId,
    string Channel,
    string Subject,
    string Body,
    NotificationStatus Status,
    DateTimeOffset CreatedAt)
{
    public NotificationResponse ToResponse() =>
        new(Id, RecipientId, Channel, Subject, Body, Status.ToString().ToLowerInvariant(), CreatedAt);
}
