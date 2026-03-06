using System.Net;

namespace NotificationService.Client;

/// <summary>
/// Thrown when the Notification Service API returns a non-success HTTP status code.
/// </summary>
public class NotificationApiException(HttpStatusCode statusCode, string responseBody) 
    : Exception($"Notification API returned {(int)statusCode} {statusCode}: {responseBody}")
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string ResponseBody { get; } = responseBody;
}
