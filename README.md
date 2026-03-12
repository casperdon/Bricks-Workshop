# Bricks Workshop Repository

This repository contains the code and resources for the Bricks Workshop — a hands-on session designed to teach participants how to build reusable libraries in .NET. The workshop covers best practices for creating modular and maintainable code, as well as how to share and distribute libraries effectively.

---

## Repository Structure

```
src/
  NotificationService.Api/					Pre-built REST API (hosted by facilitators or local)
  NotificationService.Client/				Low-level NuGet client package (distributed to participants)

workshop/
  NotificationLibrary.Workshop/				Participant starter project — this is where the work happens
  NotificationLibrary.Workshop.Consumer/	An example consumer project for our brick, used for testing and demonstration
  UserStories.md							The five user stories
  README.md									Participant instructions
```

---

## For Facilitators

### Running the API

```bash
cd src/NotificationService.Api
dotnet run
```

The API starts on `http://localhost:7000` and `https://localhost:7001` (from `launchSettings.json` — adjust as needed).

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

The OpenAPI (Swagger) spec is available at `https://localhost:7001/swagger` in Development mode.

### Workshop Flow

| Time | Activity |
|------|----------|
| 5 min | Start the API, hand out the User Stories, explain the task |
| 5 min | Teams read user stories, spot tensions, agree on interface design |
| 15–20 min | Teams implement in `NotificationLibrary.Workshop/` |
| 10–15 min | Debrief: each team explains their key design decisions; reveal how Bricks solved the same problems |

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
