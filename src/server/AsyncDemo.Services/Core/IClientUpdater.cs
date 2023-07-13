using AsyncDemo.Domain.Notifications;

namespace AsyncDemo.Services.Core;

public interface IClientUpdater
{
    Task SendGroup<T>(GroupNotification<T> notification, CancellationToken token) where T : IClientNotificationPayload;
    Task SendAll<T>(ClientNotification<T> clientNotification, CancellationToken token) where T : IClientNotificationPayload;
    Task Error<T>(string clientErrorMessage, Exception? ex, ClientNotification<T> clientNotification, CancellationToken token = default) where T : IClientNotificationPayload;
    Task Success(string clientSuccessMessage, CancellationToken token = default);
}