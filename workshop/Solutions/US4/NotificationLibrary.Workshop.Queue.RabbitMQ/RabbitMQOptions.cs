namespace NotificationLibrary.Workshop.Queue.RabbitMQ;

public class RabbitMQOptions
{
    internal string HostName { get; set; } = "localhost";
    internal int Port { get; set; } = 5672;
    internal string UserName { get; set; } = "guest";
    internal string Password { get; set; } = "guest";
    internal string VirtualHost { get; set; } = "/";
    internal string QueueName { get; set; } = "notifications";
}
