using NotificationService.Api.Models;

namespace NotificationService.Api.Store;

public interface INotificationStore
{
    void Add(NotificationRecord n);
    IEnumerable<NotificationRecord> GetAll(string? recipientId, string? channel, string? status);
    bool TryGet(string id, out NotificationRecord? n);
}