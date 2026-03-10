using Microsoft.AspNetCore.Mvc;
using NotificationLibrary.Workshop.Service;

namespace NotificationLibrary.Workshop.Consumer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetNotifications([FromQuery] string? recipientId, [FromQuery] string? channel, [FromQuery] string? status, CancellationToken cancellationToken)
    {
        var notifications = await notificationService.ListAsync(recipientId, channel, status, cancellationToken);
        return Ok(notifications);
    }

    [HttpGet]
    [Route("{notificationId}")]
    public async Task<IActionResult> GetNotification(string notificationId, CancellationToken cancellationToken)
    {
        var notification = await notificationService.GetAsync(notificationId, cancellationToken);

        return notification == null ? NotFound() : Ok(notification);
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification(string recipientId, string channel, string subject, string body, CancellationToken cancellationToken)
    {
        var response = await notificationService.SendAsync(recipientId, channel, subject, body, cancellationToken);
        return Ok(response);
    }
}
