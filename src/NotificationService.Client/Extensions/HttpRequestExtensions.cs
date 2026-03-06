using System.Net.Http.Json;

namespace NotificationService.Client.Extensions;

internal static class HttpRequestExtensions
{
    public static async Task AssertSuccess(this HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new NotificationApiException(response.StatusCode, body);
        }
    }

    public static Task<TResult> FromJson<TResult>(this HttpResponseMessage response, CancellationToken cancellationToken = default) 
        => response.Content.ReadFromJsonAsync<TResult>(cancellationToken)!;
}
