namespace NotificationService.Api.Authentication;

public static class ApiKeySecuritySetup
{
    public static void UseApiKeySecurity(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.StartsWithSegments("/openapi"))
            {
                await next(context);
                return;
            }

            var configuredKeys = app.Configuration
                .GetSection("NotificationApi:ApiKeys")
                .Get<string[]>() ?? [];

            if (configuredKeys.Length > 0)
            {
                if (!context.Request.Headers.TryGetValue("X-Api-Key", out var provided)
                    || !configuredKeys.Contains(provided.ToString()))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsJsonAsync(new { error = "Invalid or missing API key." });
                    return;
                }
            }

            await next(context);
        });
    }
}
