# Bricks Workshop Repository

This repository contains the code and resources for the Bricks Workshop — a hands-on session designed to teach participants how to build reusable libraries in .NET. The workshop covers best practices for creating modular and maintainable code, as well as how to share and distribute libraries effectively.

---

## Repository Structure

```
src/
  NotificationService.Api/        Pre-built REST API (hosted by facilitators)
  NotificationService.Client/     Low-level NuGet client package (distributed to participants)

workshop/
  NotificationLibrary.Workshop/   Participant starter project — this is where the work happens
  ConsumerCards.md                The five consumer requirement cards
  README.md                       Participant instructions
```

---

## For Facilitators

### Running the API

```bash
cd src/NotificationService.Api
dotnet run
```

The API starts on `http://localhost:5218` and `https://localhost:7078` (from `launchSettings.json` — adjust as needed).

API keys are configured in `appsettings.json`:
- `workshop-key-alpha`
- `workshop-key-beta`
- `workshop-key-gamma`

You can adjust the simulated failure rate and artificial delay:
```json
"NotificationApi": {
  "FailureRatePercent": 15,
  "ProcessingDelayMs": 80
}
```

The OpenAPI (Swagger) spec is available at `https://localhost:7080/openapi/v1.json` in Development mode.

### Workshop Flow

| Time | Activity |
|------|----------|
| 5 min | Start the API, hand out the Consumer Cards, explain the task |
| 5 min | Teams read cards, spot tensions, agree on interface design |
| 15–20 min | Teams implement in `NotificationLibrary.Workshop/` |
| 10–15 min | Debrief: each team explains their key design decisions; reveal how Bricks solved the same problems |

### Key Tensions to Watch For

The cards are designed to surface these class of problems:

| Cards | Design tension |
|-------|---------------|
| 1 + 5 | Single API key at startup vs. per-request API key |
| 2 + 3 | Retry logic baked in vs. no real HTTP in tests |
| 2 + 4 | Logging inside the library vs. must flow through host `ILogger` |
| 3 + all | Interface requirement — did they define `INotificationService`? |

### Distributing the Client Package

For a realistic setup, pack the client as a NuGet package and serve it from a local feed:

```bash
cd src/NotificationService.Client
dotnet pack -o ../../nuget-feed
```

Then point participants' `nuget.config` at the local folder. Alternatively, just use a project reference during the workshop.

---

## For Participants

See [workshop/README.md](workshop/README.md) for instructions.


## Todo:
- [] Nele op de hoogte brengen dat laptop nodig is.
- [] Powerpoint maken om te presenteren bij de uitleg.
- [] Deze oefening uitwerken en voorbeeldimplementatie maken.
- [] Bulletpoint over wat we gaan bespreken. -> verhaallijn.

### Verhaallijn:
1. Hoe zijn we hier vandaag geraakt.
2. We gaan een bibliotheek bouwen van customizable libraries.
3. We zijn tegen een hoop dingen aangelopen bij het bouwen van deze bibliotheken.
4. Voorbeeld van hoe we de problemen hebben aangepakt in Bricks en startup workshop.
