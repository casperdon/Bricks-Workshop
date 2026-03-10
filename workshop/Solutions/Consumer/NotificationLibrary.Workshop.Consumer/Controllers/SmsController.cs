using Microsoft.AspNetCore.Mvc;
using NotificationLibrary.Workshop.Sms.Service;
using System.ComponentModel.DataAnnotations;

namespace NotificationLibrary.Workshop.Consumer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmsController(ISmsNotificationService smsNotificationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery][Phone] string? recipientTelephoneNumber, [FromQuery] string? status, CancellationToken cancellationToken)
    {
        var recipientPhone = recipientTelephoneNumber != null ? new Sms.Models.TelephoneNumber(recipientTelephoneNumber) : null;
        var notifications = await smsNotificationService.ListAsync(recipientPhone, status, cancellationToken);

        return Ok(notifications);
    }

    [HttpGet]
    [Route("{notificationId}")]
    public async Task<IActionResult> Get(string notificationId, CancellationToken cancellationToken)
    {
        var notification = await smsNotificationService.GetAsync(notificationId, cancellationToken);

        return notification == null ? NotFound() : Ok(notification);
    }

    [HttpPost]
    public async Task<IActionResult> Send([Phone] string recipientTelephoneNumber, string body, CancellationToken cancellationToken)
    {
        var recipientPhone = new Sms.Models.TelephoneNumber(recipientTelephoneNumber);

        var response = await smsNotificationService.SendAsync(recipientPhone, body, cancellationToken);

        return Ok(response);
    }
}
