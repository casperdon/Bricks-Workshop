using NotificationService.Api.Models;
using System.Collections.Concurrent;

namespace NotificationService.Api.Store;

internal class InMemoryNotificationStore : INotificationStore
{
    private readonly ConcurrentDictionary<string, NotificationRecord> _store = new();

    public void Add(NotificationRecord n) => _store[n.Id] = n;

    public bool TryGet(string id, out NotificationRecord? n) =>
        _store.TryGetValue(id, out n);

    public IEnumerable<NotificationRecord> GetAll(string? recipientId, string? channel, string? status)
    {
        var q = _store.Values.AsEnumerable();

        if (!string.IsNullOrEmpty(recipientId))
            q = q.Where(n => n.RecipientId == recipientId);
        if (!string.IsNullOrEmpty(channel))
            q = q.Where(n => n.Channel == channel.ToLowerInvariant());
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<NotificationStatus>(status, true, out var s))
            q = q.Where(n => n.Status == s);

        return q.OrderByDescending(n => n.CreatedAt);
    }
}
