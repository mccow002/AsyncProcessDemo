using AsyncDemo.Domain.Notifications;

namespace AsyncDemo.Api.Models;

public record GroupNotificationModel(object Payload, string EventName, string GroupName);

public record GlobalNotificationModel(object Payload, string EventName);

public record ErrorNotificationModel(object Payload, string ErrorMessage, string Exception, string ConnectionId);

public record SuccessNotificationModel(string SuccessMessage, string ConnectionId);