using Microsoft.AspNetCore.Mvc;
using NotificationLibrary.Workshop.Email.Service;
using System.ComponentModel.DataAnnotations;

namespace NotificationLibrary.Workshop.Consumer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController(IEmailNotificationService emailNotificationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery][EmailAddress] string? recipientAdress, [FromQuery] string? status, CancellationToken cancellationToken)
    {
        var recipientEmail = recipientAdress != null ? new Email.Models.Email(recipientAdress) : null;
        var notifications = await emailNotificationService.ListAsync(recipientEmail, status, cancellationToken);

        return Ok(notifications);
    }

    [HttpGet]
    [Route("{notificationId}")]
    public async Task<IActionResult> Get(string notificationId, CancellationToken cancellationToken)
    {
        var notification = await emailNotificationService.GetAsync(notificationId, cancellationToken);

        return notification == null ? NotFound() : Ok(notification);
    }

    [HttpPost]
    public async Task<IActionResult> Send([EmailAddress] string recipientAdress, string subject, string body, CancellationToken cancellationToken)
    {
        var recipientEmail = new Email.Models.Email(recipientAdress);

        var response = await emailNotificationService.SendAsync(recipientEmail, subject, body, cancellationToken);

        return Ok(response);
    }
}
