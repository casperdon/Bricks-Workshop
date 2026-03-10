namespace NotificationLibrary.Workshop.Email.Models;

public class EmailNotificationResponse
{
    public string Id { get; set; } = string.Empty;

    public Email RecipientEmail { get; set; } = default!;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
}
