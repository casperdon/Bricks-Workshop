namespace NotificationLibrary.Workshop.Sms.Models;

public class SmsNotificationResponse
{
    public string Id { get; set; } = string.Empty;

    public TelephoneNumber RecipientPhoneNumber { get; set; } = default!;

    public string Body { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
}
