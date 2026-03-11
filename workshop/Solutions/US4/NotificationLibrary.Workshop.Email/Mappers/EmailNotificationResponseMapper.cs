using NotificationLibrary.Workshop.Email.Models;
using NotificationService.Client.Responses;

namespace NotificationLibrary.Workshop.Email.Mappers;

internal static class EmailNotificationResponseMapper
{
    public static EmailNotificationResponse Map(NotificationResponse response)
        => new()
        {
            Id = response.Id,
            RecipientEmail = new Models.Email(response.RecipientId),
            Subject = response.Subject!,
            Body = response.Body,
            Status = response.Status,
            CreatedAt = response.CreatedAt
        };
}
