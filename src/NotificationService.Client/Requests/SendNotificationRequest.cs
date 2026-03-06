namespace NotificationService.Client.Requests;

public class SendNotificationRequest
{
    /// <summary>The ID of the user or account that should receive this notification.</summary>
    public string RecipientId { get; set; } = string.Empty;

    /// <summary>Delivery channel. Valid values: "email", "in-app", "push".</summary>
    public string Channel { get; set; } = string.Empty;

    /// <summary>Optional subject line (primarily used for email).</summary>
    public string? Subject { get; set; }

    /// <summary>The notification body text.</summary>
    public string Body { get; set; } = string.Empty;
}
