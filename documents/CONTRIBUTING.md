# Contributing to WorkOwl

Velkommen til WorkOwl-prosjektet! Dette dokumentet beskriver hvordan vi jobber sammen som team — branch-konvensjoner, PR-regler, kodestandard og commit-format.

---

## Innholdsfortegnelse

1. [Branch-konvensjoner](#branch-konvensjoner)
2. [Commit-meldinger](#commit-meldinger)
3. [Pull Requests](#pull-requests)
4. [Kodekonvensjoner](#kodekonvensjoner)
5. [Prosjektstruktur](#prosjektstruktur)
6. [Kjøre prosjektet lokalt](#kjøre-prosjektet-lokalt)

---

## Branch-konvensjoner

Vi bruker `main` og `develop` som hovedgrener. All utvikling skjer i feature-branches som merges inn i `develop` via Pull Request.

### Grener

| Gren | Formål |
|---|---|
| `main` | Stabil produksjonskode — merges kun fra `develop` ved sprint-slutt |
| `develop` | Aktiv utviklingsgren — alle PRs går hit |
| `feature/...` | Ny funksjonalitet |
| `bugfix/...` | Feilretting |
| `hotfix/...` | Kritisk feilretting direkte mot `main` |
| `chore/...` | Oppsett, avhengigheter, konfig — ingen funksjonell endring |

### Navngivning

```
feature/add-auth-jwt
feature/competency-status-background-job
bugfix/department-null-reference
chore/update-ef-packages
```

Bruk alltid **kebab-case** og **engelsk**. Vær beskrivende — unngå navn som `feature/fix` eller `feature/test`.

### Regler

- Ingen direkte push til `main` eller `develop`
- Alle endringer går via Pull Request
- Slett feature-branch etter merge

---

## Commit-meldinger

Vi følger [Conventional Commits](https://www.conventionalcommits.org/):

```
<type>: <kort beskrivelse>
```

### Typer

| Type | Når |
|---|---|
| `feat` | Ny funksjonalitet |
| `fix` | Feilretting |
| `docs` | Dokumentasjonsendringer |
| `refactor` | Refaktorering uten funksjonell endring |
| `test` | Legger til eller endrer tester |
| `chore` | Byggesystem, avhengigheter, konfig |

### Eksempler

```
feat: add JWT authentication endpoint
fix: resolve null reference in DepartmentService
docs: update ERD diagram
refactor: extract email sending to IEmailService
test: add integration tests for CompetencyRepository
chore: add docker-compose for local PostgreSQL
```

- Bruk **engelsk** i commit-meldinger
- Bruk **imperativ form** ("add", ikke "added" eller "adding")
- Hold første linje under 72 tegn

---

## Pull Requests

### Før du oppretter en PR

- [ ] Koden kompilerer uten feil
- [ ] Alle eksisterende tester passerer
- [ ] Du har skrevet tester for ny funksjonalitet der det er relevant
- [ ] Koden følger konvensjonene i dette dokumentet

### PR-regler

- Minimum **1 godkjenning** fra et annet teammedlem før merge
- Ingen self-merge — du kan ikke godkjenne din egen PR
- Base branch skal alltid være `develop` (aldri direkte til `main`)
- Bruk en beskrivende tittel som følger commit-konvensjonen

### PR-beskrivelse

Fyll ut følgende når du oppretter en PR:

```
## Hva er gjort?
Kort beskrivelse av hva denne PRen inneholder.

## Hvordan teste?
Steg for å verifisere at endringene fungerer.

## Relaterte oppgaver
SCRUM-XX
```

---

## Kodekonvensjoner

### Generelt

- **Kommentarer:** Norsk
- **Kode (variabelnavn, metodenavn, klassenavn):** Engelsk
- **API-endepunkter:** Engelsk

### C# / ASP.NET Core

Følg standard C#-konvensjoner:

| Element | Konvensjon | Eksempel |
|---|---|---|
| Klasser | PascalCase | `DepartmentService` |
| Metoder | PascalCase | `GetDepartmentById` |
| Properties | PascalCase | `IsActive` |
| Private felt | camelCase med `_` | `_departmentRepository` |
| Lokale variabler | camelCase | `departmentId` |
| Interfaces | Prefiks med `I` | `IDepartmentService` |
| Konstanter | PascalCase | `MaxRetryCount` |

### Result-pattern

Alle service-metoder skal returnere `Result` eller `Result<T>` fra `WorkOwl.Shared`:

```csharp
// Riktig
public async Task<Result<DepartmentDto>> GetByIdAsync(int id)

// Ikke dette
public async Task<DepartmentDto?> GetByIdAsync(int id)
```

### Mappestruktur per feature

Hver feature skal følge denne strukturen:

```
Features/Departments/
├── DepartmentsController.cs
├── DepartmentService.cs
├── IDepartmentService.cs
├── DepartmentRepository.cs
├── IDepartmentRepository.cs
├── Department.cs               ← EF-entitet
├── CreateDepartmentDto.cs
├── UpdateDepartmentDto.cs
└── DepartmentResponseDto.cs
```

### Blazor / Frontend

- Komponenter: PascalCase (`DepartmentList.razor`)
- Én komponent per fil
- Bruk `@inject` fremfor konstruktørinjeksjon i komponenter

---

## Prosjektstruktur

```
WorkOwl.sln
├── WC.Backend/                 ← ASP.NET Core Web API
│   ├── Common/
│   │   └── Controllers/
│   │       └── BaseController.cs
│   ├── Features/
│   │   ├── Auth/
│   │   ├── Competencies/
│   │   ├── Departments/
│   │   ├── Documents/
│   │   ├── Equipment/
│   │   ├── Notifications/
│   │   ├── Onboarding/
│   │   ├── Reports/
│   │   ├── Requirements/
│   │   ├── Roles/
│   │   └── Users/
│   └── Infrastructure/
│       ├── Persistence/
│       ├── BackgroundJobs/
│       ├── Email/
│       ├── FileStorage/
│       └── Audit/
├── WC.Frontend/                ← Blazor Server
├── WC.Shared/                  ← Delte klasser (Result, Enums)
└── WC.Tests/                   ← Enhet- og integrasjonstester
```

---

## Kjøre prosjektet lokalt

### Krav

- .NET 10 SDK
- Docker Desktop

### Oppsett

1. Klon repoet og bytt til `develop`:
```bash
git clone <repo-url>
cd workOwl
git checkout develop
```

2. Start PostgreSQL med Docker:
```bash
docker-compose up -d
```

3. Kjør migrasjoner:
```bash
cd WC.Backend
dotnet ef database update
```

4. Start backend:
```bash
dotnet run --project WC.Backend
```

5. Start frontend:
```bash
dotnet run --project WC.Frontend
```

API kjører på `https://localhost:5001`, frontend på `https://localhost:5000`.
