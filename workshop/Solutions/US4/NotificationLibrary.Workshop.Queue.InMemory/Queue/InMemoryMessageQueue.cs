using System.Collections.Concurrent;
using NotificationLibrary.Workshop.Queue.Services;
using NotificationService.Client.Requests;

namespace NotificationLibrary.Workshop.Queue.InMemory.Queue;


internal class InMemoryMessageQueue : IMessageBus, IMessageQueueReceiver
{
    private readonly ConcurrentQueue<SendNotificationRequest> _queue = new();
    private readonly SemaphoreSlim _signal = new(0);

    public Task PublishAsync(SendNotificationRequest message, CancellationToken cancellationToken = default)
    {
        _queue.Enqueue(message);
        _signal.Release();
        return Task.CompletedTask;
    }

    public async Task<SendNotificationRequest?> TryDequeueAsync(CancellationToken cancellationToken)
    {
        await _signal.WaitAsync(cancellationToken).ConfigureAwait(false);

        return _queue.TryDequeue(out var message) ? message : null;
    }
}
