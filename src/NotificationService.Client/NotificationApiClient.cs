using System.Net.Http.Json;
using NotificationService.Client.Extensions;
using NotificationService.Client.Requests;
using NotificationService.Client.Responses;

namespace NotificationService.Client;

/// <summary>
/// Low-level HTTP client for the Notification Service API.
///
/// This client handles raw HTTP communication only — it has no retry logic,
/// no logging, no DI integration, and no configuration abstractions.
/// It is the building block; your library wraps it.
/// </summary>
/// <param name="httpClient">
///   An <see cref="HttpClient"/> with its <c>BaseAddress</c> already set
///   to the Notification Service root URL (e.g. https://localhost:7080/).
/// </param>
/// <param name="apiKey">The API key issued for your application.</param>
public class NotificationApiClient(HttpClient httpClient, string apiKey)
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly string _apiKey = !string.IsNullOrWhiteSpace(apiKey)
            ? apiKey
            : throw new ArgumentException("API key must not be empty.", nameof(apiKey));

    /// <summary>Sends a notification. Returns the created notification on success.</summary>
    /// <exception cref="NotificationApiException">Thrown when the API returns a non-success status code.</exception>
    public async Task<NotificationResponse> SendAsync(
        SendNotificationRequest request,
        CancellationToken cancellationToken = default)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "notifications")
        {
            Content = JsonContent.Create(request)
        };
        httpRequest.Headers.Add("X-Api-Key", _apiKey);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        await response.AssertSuccess(cancellationToken);

        return await response.FromJson<NotificationResponse>(cancellationToken);
    }

    /// <summary>
    /// Gets a notification by ID. Returns <c>null</c> if not found (404).
    /// </summary>
    /// <exception cref="NotificationApiException">Thrown for non-success, non-404 responses.</exception>
    public async Task<NotificationResponse?> GetAsync(
        string notificationId,
        CancellationToken cancellationToken = default)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"notifications/{notificationId}");
        httpRequest.Headers.Add("X-Api-Key", _apiKey);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        await response.AssertSuccess(cancellationToken);

        return await response.FromJson<NotificationResponse>(cancellationToken);
    }

    /// <summary>Lists notifications, optionally filtered by recipient, channel, or status.</summary>
    /// <param name="recipientId">Filter by recipient ID.</param>
    /// <param name="channel">Filter by channel ("email", "in-app", "push").</param>
    /// <param name="status">Filter by status ("pending", "sent", "failed").</param>
    public async Task<IReadOnlyList<NotificationResponse>> ListAsync(
        string? recipientId = null,
        string? channel = null,
        string? status = null,
        CancellationToken cancellationToken = default)
    {
        var queryParts = new List<string>();
        if (recipientId is not null) queryParts.Add($"recipientId={Uri.EscapeDataString(recipientId)}");
        if (channel     is not null) queryParts.Add($"channel={Uri.EscapeDataString(channel)}");
        if (status      is not null) queryParts.Add($"status={Uri.EscapeDataString(status)}");

        var url = queryParts.Count > 0
            ? $"notifications?{string.Join("&", queryParts)}"
            : "notifications";

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        httpRequest.Headers.Add("X-Api-Key", _apiKey);

        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        await response.AssertSuccess(cancellationToken);

        return await response.FromJson<List<NotificationResponse>>(cancellationToken);
    }
}
