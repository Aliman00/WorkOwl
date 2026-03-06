# WorkOwl — API-dokumentasjon

> **Primær API-dokumentasjon:** Swagger UI er alltid oppdatert og tilgjengelig når applikasjonen kjører.
> Dette dokumentet er et supplement som forklarer autentisering, tillatelser og konvensjoner.

---

## Swagger UI

| Miljø | URL |
|---|---|
| Lokalt | `https://localhost:5001/swagger` |
| Produksjon | `https://<din-azure-url>/swagger` |

---

## Autentisering i Swagger UI

Alle endepunkter krever gyldig JWT med unntak av `/auth/login`.

**Slik autentiserer du i Swagger:**

1. Kall `POST /auth/login` med e-post og passord
2. Kopier `accessToken` fra responsen
3. Trykk **Authorize** øverst i Swagger UI
4. Lim inn token som: `Bearer <accessToken>`
5. Trykk **Authorize** — alle påfølgende kall vil inkludere tokenet

**Token-levetid:**
- Access token: 15 minutter
- Refresh token: 7 dager — forny via `POST /auth/refresh`

---

## Tillatelser (Permissions)

Roller i WorkOwl er samlinger av granulære tillatelser. API-endepunkter sjekker tillatelse, ikke rollenavn.

| Tillatelse | Beskrivelse |
|---|---|
| `users:read` | Lese brukere og avdelinger |
| `users:write` | Opprette og redigere brukere og avdelinger |
| `users:delete` | Deaktivere brukere (soft delete) |
| `roles:read` | Lese roller og tillatelser |
| `roles:write` | Opprette og redigere roller, tildele tillatelser |
| `competencies:read` | Lese kompetansebevis |
| `competencies:write` | Registrere og redigere kompetansebevis |
| `competencies:delete` | Slette kompetansebevis |
| `responsibilities:read` | Lese ansvarsområder |
| `responsibilities:write` | Opprette og tildele ansvarsområder |
| `documents:read` | Lese HMS-dokumenter og signeringsstatus |
| `documents:write` | Laste opp og oppdatere HMS-dokumenter |
| `documents:acknowledge` | Signere HMS-dokument («Lest og forstått») |
| `requirements:read` | Lese stillingskrav |
| `requirements:write` | Opprette og redigere stillingskrav |
| `requirements:confirm` | Ansatt bekrefter stillingskrav |
| `requirements:approve` | Leder godkjenner stillingskrav |
| `equipment:read` | Lese utstyrslogg |
| `equipment:write` | Registrere og oppdatere utstyr |
| `onboarding:read` | Lese onboarding-maler og sjekklister |
| `onboarding:write` | Opprette og tildele onboarding-maler |
| `onboarding:complete` | Markere onboarding-oppgaver som fullført |
| `reports:read` | Lese og eksportere rapporter |
| `audit:read` | Lese revisjonslogg |

---

## Konvensjoner

### Base URL
```
https://localhost:5001/api
```

### Responskoder

| Kode | Betydning |
|---|---|
| `200 OK` | Vellykket GET / PUT |
| `201 Created` | Vellykket POST — ressurs opprettet |
| `204 No Content` | Vellykket DELETE |
| `400 Bad Request` | Valideringsfeil — se `errors` i respons |
| `401 Unauthorized` | Manglende eller ugyldig JWT |
| `403 Forbidden` | Gyldig JWT, men manglende tillatelse |
| `404 Not Found` | Ressurs ikke funnet |
| `500 Internal Server Error` | Uventet serverfeil |

### Feilrespons-format
```json
{
  "error": "Kompetansebevis ikke funnet",
  "errorType": "NotFound"
}
```

### Paginering
Endepunkter som returnerer lister støtter valgfrie query-parametere:
```
GET /competencies?page=1&pageSize=20
```

### Rapporteksport
Rapportendepunkter støtter `?format=pdf` eller `?format=csv`:
```
GET /reports/competence?format=pdf
GET /reports/equipment?format=csv
```

---

## Endepunkter — oversikt

### Autentisering

| Metode | Sti | Beskrivelse | Auth |
|---|---|---|---|
| POST | `/auth/login` | Logg inn, returner access + refresh token | Nei |
| POST | `/auth/refresh` | Forny access token med refresh token | Nei |
| POST | `/auth/logout` | Ugyldiggjør refresh token | Ja |
| GET | `/auth/me` | Hent innlogget brukers profil og tillatelser | Ja |

### Brukere

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/users` | List alle aktive brukere | `users:read` |
| POST | `/users` | Opprett ny bruker | `users:write` |
| GET | `/users/{id}` | Hent én bruker | `users:read` |
| PUT | `/users/{id}` | Oppdater bruker | `users:write` |
| DELETE | `/users/{id}` | Deaktiver bruker (soft delete) | `users:delete` |
| POST | `/users/{id}/roles` | Tildel rolle til bruker | `roles:write` |
| POST | `/users/{id}/responsibilities` | Knytt bruker til ansvarsområde | `responsibilities:write` |

### Avdelinger

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/departments` | List alle avdelinger | `users:read` |
| POST | `/departments` | Opprett ny avdeling | `users:write` |
| PUT | `/departments/{id}` | Oppdater avdeling | `users:write` |
| DELETE | `/departments/{id}` | Slett avdeling | `users:delete` |

### Roller og tillatelser

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/roles` | List alle roller | `roles:read` |
| POST | `/roles` | Opprett ny rolle | `roles:write` |
| PUT | `/roles/{id}` | Oppdater rolle | `roles:write` |
| POST | `/roles/{id}/permissions` | Tildel tillatelser til rolle | `roles:write` |

### Kompetansebevis

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/competencies` | List kompetansebevis (filtrerbart: `?userId=&status=`) | `competencies:read` |
| POST | `/competencies` | Registrer nytt bevis | `competencies:write` |
| GET | `/competencies/{id}` | Hent ett bevis | `competencies:read` |
| PUT | `/competencies/{id}` | Oppdater bevis | `competencies:write` |
| DELETE | `/competencies/{id}` | Slett bevis | `competencies:delete` |
| GET | `/competencies/expiring` | List bevis som utløper innen 60 dager | `competencies:read` |

### Ansvarsområder

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/responsibilities` | List alle ansvarsområder | `responsibilities:read` |
| POST | `/responsibilities` | Opprett nytt ansvarsområde | `responsibilities:write` |

### HMS-dokumenter

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/documents` | List alle HMS-dokumenter | `documents:read` |
| POST | `/documents` | Last opp eller lenke til dokument | `documents:write` |
| PUT | `/documents/{id}` | Oppdater dokument (øker versjon) | `documents:write` |
| POST | `/documents/{id}/acknowledge` | Signer «Lest og forstått» | `documents:acknowledge` |
| GET | `/documents/{id}/acknowledgements` | Se hvem som har signert | `documents:read` |

### Stillingskrav

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/requirements` | List alle stillingskrav | `requirements:read` |
| POST | `/requirements` | Opprett nytt krav | `requirements:write` |
| GET | `/requirements/user/{id}` | Hent krav for én bruker | `requirements:read` |
| POST | `/requirements/{id}/confirm/employee` | Ansatt bekrefter krav | `requirements:confirm` |
| POST | `/requirements/{id}/confirm/manager` | Leder godkjenner krav | `requirements:approve` |

### Utstyrslogg

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/equipment` | List alt utlevert utstyr | `equipment:read` |
| POST | `/equipment` | Registrer utlevering | `equipment:write` |
| PUT | `/equipment/{id}` | Oppdater utstyrspost | `equipment:write` |
| POST | `/equipment/{id}/return` | Marker utstyr som returnert | `equipment:write` |
| GET | `/equipment/user/{id}` | Hent utstyr for én bruker | `equipment:read` |

### Onboarding og offboarding

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/onboarding/templates` | List alle maler | `onboarding:read` |
| POST | `/onboarding/templates` | Opprett ny mal | `onboarding:write` |
| POST | `/onboarding/assign` | Tildel mal til ansatt | `onboarding:write` |
| GET | `/onboarding/user/{id}` | Hent aktiv onboarding for bruker | `onboarding:read` |
| PUT | `/onboarding/tasks/{id}/complete` | Marker oppgave som fullført | `onboarding:complete` |

### Varsler

| Metode | Sti | Beskrivelse | Auth |
|---|---|---|---|
| GET | `/notifications` | Hent varsler for innlogget bruker | Ja |
| PUT | `/notifications/{id}/read` | Marker ett varsel som lest | Ja |
| PUT | `/notifications/read-all` | Marker alle varsler som lest | Ja |

### Rapporter

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/reports/competence` | Kompetansestatus per avdeling (`?departmentId=`) | `reports:read` |
| GET | `/reports/equipment` | Utstyrsoversikt per ansatt | `reports:read` |
| GET | `/reports/acknowledgements` | Lest og forstått-status per dokument | `reports:read` |

> Alle rapporter støtter `?format=pdf` og `?format=csv`

### Revisjonslogg

| Metode | Sti | Beskrivelse | Tillatelse |
|---|---|---|---|
| GET | `/audit-log` | Hent revisjonslogg (filtrerbart: `?userId=&entityType=`) | `audit:read` |

---

## Eksempler

### Innlogging
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@bedrift.no",
  "password": "passord123"
}
```

```json
{
  "accessToken": "eyJhbGci...",
  "refreshToken": "dGhpcyBp...",
  "expiresIn": 900
}
```

### Registrere kompetansebevis
```http
POST /api/competencies
Authorization: Bearer eyJhbGci...
Content-Type: application/json

{
  "userId": 5,
  "typeId": 2,
  "issuedDate": "2025-01-15",
  "expiryDate": "2027-01-15",
  "notes": "Truckførerbevis klasse T2"
}
```

### Hente utløpende bevis
```http
GET /api/competencies/expiring
Authorization: Bearer eyJhbGci...
```

### Eksportere kompetanserapport som PDF
```http
GET /api/reports/competence?departmentId=3&format=pdf
Authorization: Bearer eyJhbGci...
```
