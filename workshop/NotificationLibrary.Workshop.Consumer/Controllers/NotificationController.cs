using Microsoft.AspNetCore.Mvc;

namespace NotificationLibrary.Workshop.Consumer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
}
