namespace NotificationLibrary.Workshop.Queue.RabbitMQ;

public class RabbitMQOptionsBuilder
{
    private readonly RabbitMQOptions _options = new RabbitMQOptions();

    public RabbitMQOptionsBuilder WithHostName(string hostName)
    {
        _options.HostName = hostName;
        return this;
    }

    public RabbitMQOptionsBuilder WithPort(int port)
    {
        _options.Port = port;
        return this;
    }

    public RabbitMQOptionsBuilder WithUserName(string userName)
    {
        _options.UserName = userName;
        return this;
    }

    public RabbitMQOptionsBuilder WithPassword(string password)
    {
        _options.Password = password;
        return this;
    }

    public RabbitMQOptionsBuilder WithVirtualHost(string virtualHost)
    {
        _options.VirtualHost = virtualHost;
        return this;
    }

    public RabbitMQOptionsBuilder WithQueueName(string queueName)
    {
        _options.QueueName = queueName;
        return this;
    }

    public RabbitMQOptions Build() => _options;
}
