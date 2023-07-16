using AsyncDemo.Domain.Notifications;

namespace AsyncDemo.Api.Models;

public record ClientNotificationModel(object Payload, string EventName);

public record GroupNotificationModel(ClientNotificationModel Notification, string GroupName);

public record SuccessNotificationModel(string SuccessMessage, string ConnectionId);

public record ErrorNotificationModel(ErrorContentModel Payload, string ConnectionId);

public record ErrorContentModel(ClientNotificationModel Notification, string ErrorMessage, string Exception);