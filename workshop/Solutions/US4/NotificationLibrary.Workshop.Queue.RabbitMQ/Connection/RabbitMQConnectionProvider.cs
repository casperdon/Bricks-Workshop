using RabbitMQ.Client;

namespace NotificationLibrary.Workshop.Queue.RabbitMQ.Connection;

internal sealed class RabbitMQConnectionProvider : IAsyncDisposable
{
    private readonly RabbitMQOptions _options;
    private IConnection? _connection;
    private readonly SemaphoreSlim _lock = new(1, 1);

    public RabbitMQConnectionProvider(RabbitMQOptions options) => _options = options;

    public async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { IsOpen: true })
            return _connection;

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost
            };

            _connection = await factory.CreateConnectionAsync(cancellationToken);
            return _connection;
        }
        finally
        {
            _lock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
        _lock.Dispose();
    }
}
