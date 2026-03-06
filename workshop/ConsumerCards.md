# Consumer Requirement Cards

> **Facilitator note**: Print these cards (one per team) or share them digitally.
> Each team receives **all five cards** and must design a library that satisfies every card simultaneously.
> The tension between cards is intentional — that's the exercise.

---

## Card 1 — Team Alpha | Consumer Web App

**Who we are**: A standard ASP.NET Core web application. We have a `Program.cs` with a DI setup and appsettings.json for all our configuration.

**What we need from your library**:

- We want to add your library in **one or two lines** in `Program.cs`. No ceremony.
- We store configuration in `appsettings.json`. We should be able to put your settings there and your library should pick them up automatically.
- We don't want to think about `HttpClient` lifecycle or `IHttpClientFactory` — that should be handled for us.

**Example of what we expect**:
```csharp
// Program.cs
builder.Services.AddNotifications(builder.Configuration);
```

```json
// appsettings.json
{
  "Notifications": {
    "BaseUrl": "https://notifications.example.com",
    "ApiKey": "workshop-key-alpha"
  }
}
```

**What we absolutely don't want**: Having to manually `new` up classes. We want everything to go through DI.

---

## Card 2 — Team Beta | Background Worker Service

**Who we are**: A .NET Worker Service that runs scheduled jobs — sending digest notifications, reminders, and alerts. We sometimes call the Notification Service hundreds of times per batch.

**What we need from your library**:

- **Automatic retry with exponential backoff** when the API returns a failure. We have observed that the API sometimes returns failures transiently. A retry of 3 attempts is sufficient.
- **Structured logging** of each notification sent, including the notification ID returned by the API and whether it succeeded or was retried.
- We register your library the same way as Team Alpha — one line in our `IHostBuilder` setup.

**Example of our usage**:
```csharp
// Injected via DI
public class DigestJob(INotificationService notifications)
{
    public async Task RunAsync(IEnumerable<string> recipientIds)
    {
        foreach (var id in recipientIds)
        {
            await notifications.SendAsync(new SendRequest
            {
                RecipientId = id,
                Channel = "email",
                Subject = "Your daily digest",
                Body = "Here is your summary..."
            });
        }
    }
}
```

**What we absolutely don't want**: Exceptions thrown for every transient failure. We want retry to happen transparently.

---

## Card 3 — Team Gamma | Integration Tests

**Who we are**: We write integration tests for an application that uses your library. We test in CI and we do **not** want to call the real Notification Service API during tests — it's slow, flaky, and we don't want test data in production.

**What we need from your library**:

- A way to **substitute a fake/in-memory implementation** of the notification service without touching the application code.
- The fake should let us **assert** that specific notifications were sent (recipient, channel, content).
- The substitution should work cleanly with the DI container.

**Example of our test setup**:
```csharp
// In test setup
services.AddNotificationsFake(); // or ReplaceWithFake(), or similar

// In a test
var fake = services.GetRequiredService<IFakeNotificationService>(); // or similar
await sut.DoSomethingThatSendsANotification();

Assert.Single(fake.SentNotifications);
Assert.Equal("user-123", fake.SentNotifications[0].RecipientId);
```

**What we absolutely don't want**: Having to mock `HttpClient` directly. That's fragile and painful.

---

## Card 4 — Team Delta | Enterprise Application

**Who we are**: A large line-of-business application with strict standards set by our architecture team. We use **Serilog** for structured logging, configured company-wide with enrichers, sinks, and correlation IDs. All third-party libraries must route their logging through our existing `ILogger` setup — they must not configure their own loggers.

**What we need from your library**:

- All internal log output uses `ILogger<T>` from `Microsoft.Extensions.Logging` — nothing else. We will plug in our Serilog provider at the host level.
- **No forced dependencies** on specific logging packages (no `Serilog.*`, no `NLog.*` in your package).
- Configurable log levels — we want to be able to suppress verbose logs in production.
- Full **correlation ID / Activity propagation** in HTTP headers when calling the API (we use `Activity.Current` for distributed tracing).

**Example of our setup**:
```csharp
// We wire up Serilog ourselves — your library just uses ILogger<T> and it will flow through
builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddNotifications(builder.Configuration); // same as Team Alpha
```

**What we absolutely don't want**: A library that registers its own logging pipeline or takes a logger as a constructor argument from the consumer. Everything must go through the DI-registered `ILogger<T>`.

---

## Card 5 — Team Epsilon | Multi-Tenant SaaS Platform

**Who we are**: A multi-tenant platform where each tenant has their **own API key** for the Notification Service. We have dozens of tenants. All tenant configuration is loaded at runtime from a database — we cannot use `appsettings.json` for API keys.

**What we need from your library**:

- At runtime, per-request, we must be able to **specify which tenant's API key** to use when sending a notification.
- We cannot register one API key at startup — the key must be passed in on each call.
- This should still use DI and `INotificationService` injection — we don't want to bypass the rest of your library's features (retry, logging, etc.).

**Example of our usage**:
```csharp
public class TenantNotificationHandler(INotificationService notifications)
{
    public async Task NotifyAsync(Tenant tenant, string recipientId, string message)
    {
        await notifications.SendAsync(new SendRequest
        {
            RecipientId = recipientId,
            Channel     = "in-app",
            Body        = message,
            // We need to pass the tenant's API key somehow — you decide how
        }, apiKey: tenant.NotificationApiKey);
    }
}
```

**What we absolutely don't want**: Having to instantiate a new, fully configured `INotificationService` for each tenant. That defeats the purpose of your library.

---

## Design Tensions Cheat Sheet (for debrief)

| Cards | Tension they create |
|-------|-------------------|
| 1 + 5 | Single API key at startup (Card 1) vs. per-request API key (Card 5) |
| 2 + 3 | Retry logic baked in (Card 2) vs. no real HTTP in tests (Card 3) — does retry run against the fake? |
| 2 + 4 | Logging everywhere (Card 2) vs. logging must flow through host ILogger (Card 4) — forced dependency risk |
| 1 + 4 | One-line setup (Card 1) vs. no magic defaults for logging (Card 4) |
| 3 + all | An `INotificationService` interface is required for mockability — did you define one? |
