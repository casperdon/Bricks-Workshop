# Workshop: Don’t Reinvent the Brick: Reusable Libraries in .NET

> This is the participant folder. Everything you need to complete the workshop exercise is here.

## Your Task

You and your team have been handed a pre-built **Notification Service** REST API and a low-level client package (`NotificationService.Client`) that wraps it.

**Your job**: Build a proper library on top of that client — one that many different internal teams can all use with their own requirements.

Read the [UserStories](UserStories.md) carefully before you write a single line of code.

---

## Step 1 — Read the user stories (5 minutes)

Open [UserStories.md](UserStories.md). Read all five user stories as a team. Note the tensions between them. You don't need to solve everything — but you need to *understand* the problem before you design.

Key questions:
- Which user stories have **conflicting** expectations?
- Which design decisions have to be made **first** because everything else depends on them?

---

## Step 2 — Design before you code (5 minutes)

Before touching the skeleton files, agree as a team on:

1. **The interface shape** — what does `INotificationService` look like?
2. **The options class** — what is required? What has defaults? Builder pattern or flat properties?
3. **The DI registration** — one method? Two? With optional parameters?

---

## Step 3 — Implement (15–20 minutes)

The starter project (`NotificationLibrary.Workshop/`) already references:
- `NotificationService.Client` (`ThunderBean.NotificationService.Client`) — the raw API client
- `Microsoft.Extensions.DependencyInjection.Abstractions`
- `Microsoft.Extensions.Http` (for `IHttpClientFactory`)
- `Microsoft.Extensions.Logging.Abstractions`
- `Microsoft.Extensions.Options`

Skeleton files to fill in:
| File | What it's for |
|------|--------------|
| `INotificationService.cs` | The public contract consumers depend on |
| `NotificationLibraryOptions.cs` | Configuration options |
| `ServiceCollectionExtensions.cs` | DI registration — the "one line in Program.cs" |

A basic implementation class (`NotificationService.cs`) has been provided, since the focus of the workshop is on the *library design* rather than the API integration.

---

## Notification Service API

The API is already running. Base URL: **http://localhost:7000** (HTTP) or **https://localhost:7001** (HTTPS).

Available API keys (for testing):
- `workshop-key-alpha`
- `workshop-key-beta`
- `workshop-key-gamma`

### Endpoints

| Method | Path | Description |
|--------|------|-------------|
| `POST` | `/notifications` | Send a notification |
| `GET` | `/notifications/{id}` | Get a notification by ID |
| `GET` | `/notifications` | List notifications |

All endpoints require the `X-Api-Key` header.

### POST /notifications — Request body

```json
{
  "recipientId": "user-123",
  "channel": "email",
  "subject": "Hello",
  "body": "This is the body."
}
```

Valid channels: `"email"`, `"sms"`, `"push"`

### POST /notifications — Response (201 Created)

```json
{
  "id": "a1b2c3d4...",
  "recipientId": "user-123",
  "channel": "email",
  "subject": "Hello",
  "body": "This is the body.",
  "status": "sent",
  "createdAt": "2026-03-06T10:00:00Z"
}
```

> **Note**: The API simulates a ~15% transient failure rate (`"status": "failed"`).
> This is intentional — it makes the retry/resilience requirement real.

---

## Exploring the client

The `NotificationApiClient` class (from `NotificationService.Client`) is intentionally minimal:

```csharp
var client = new NotificationApiClient(httpClient, apiKey: "workshop-key-alpha");

var response = await client.SendAsync(new SendNotificationRequest
{
    RecipientId = "user-123",
    Channel     = "email",
    Subject     = "Hello",
    Body        = "This is the notification body."
});

Console.WriteLine(response.Id);    // the notification ID
Console.WriteLine(response.Status); // "sent" or "failed"
```

It will throw `NotificationApiException` on non-success HTTP responses.

---

## Tips

- **Start with the interface** — everything flows from it.
- **`IHttpClientFactory`** is your friend for `HttpClient` lifetime management. Use `services.AddHttpClient<T>()`.
- **`IOptions<T>`** is the standard .NET way to bind configuration sections. Where does it fall short for Card 5?
- **Don't fake `HttpClient`** for Card 3 — fake `INotificationService` instead. That's the point of having an interface.
- For retry, consider **Polly** (via `Microsoft.Extensions.Http.Resilience` or the classic `Polly` package).
