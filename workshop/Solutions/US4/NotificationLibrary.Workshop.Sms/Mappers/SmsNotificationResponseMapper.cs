using NotificationLibrary.Workshop.Sms.Models;
using NotificationService.Client.Responses;

namespace NotificationLibrary.Workshop.Sms.Mappers;

internal static class SmsNotificationResponseMapper
{
    public static SmsNotificationResponse Map(NotificationResponse response)
        => new()
        {
            Id = response.Id,
            RecipientPhoneNumber = new TelephoneNumber(response.RecipientId),
            Body = response.Body,
            Status = response.Status,
            CreatedAt = response.CreatedAt
        };
}
