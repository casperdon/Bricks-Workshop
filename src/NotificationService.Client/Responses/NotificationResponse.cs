namespace NotificationService.Client.Responses;

public class NotificationResponse
{
    public string Id { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string? Subject { get; set; }
    public string Body { get; set; } = string.Empty;

    /// <summary>One of: "pending", "sent", "failed".</summary>
    public string Status { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
}
