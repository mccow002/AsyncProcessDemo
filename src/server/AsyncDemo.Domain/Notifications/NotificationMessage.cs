namespace AsyncDemo.Domain.Notifications;

public record ClientNotification<T>(T Payload, string EventName) where T : IClientNotificationPayload;

public record GroupNotification<T>(ClientNotification<T> Notification, string GroupName) where T : IClientNotificationPayload;

public record ErrorNotification<T>(ClientNotification<T> Notification, string ErrorMessage, string Exception) where T : IClientNotificationPayload;

public record SuccessNotification(string SuccessMessage);