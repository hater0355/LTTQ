# PayOS Payments API

The WinForms application does not contain PayOS credentials. Run this ASP.NET Core API beside the SQL database and publish it behind an HTTPS reverse proxy with a publicly reachable webhook route at `/webhooks/payos`.

## Local setup

1. Start the `PaymentsApi` project once. It creates `PaymentsApi/appsettings.Local.json` with local defaults, a random client key, and a separate random signing key for short-lived cashier sessions.
2. Open that file and paste the PayOS channel values into `PayOS.ClientId`, `PayOS.ApiKey`, and `PayOS.ChecksumKey`. Save it, then restart the API.
3. Configure the channel webhook in the PayOS portal as `https://<public-host>/webhooks/payos`. For a local API, expose port `5080` through an HTTPS tunnel or use a deployed HTTPS host. PayOS must be able to reach this address.
4. Start the WinForms project. It reads the local API URL, client key, and SQL connection string from the same local settings file. On successful cashier login, the desktop exchanges the credentials for a short-lived, cashier-bound API session; the shared client key alone cannot create or cancel sales.

The local settings file is ignored by Git and must not be committed. The local return and cancel pages are served at `/payos/return` and `/payos/cancel`. They only show a message; payment status is confirmed by the verified webhook and the desktop app's status polling.

The current database connection defaults to `Server=.;Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5;`. Set `ConnectionStrings:CinemaDb` in the local file if your SQL Server uses a different connection string. Both the API and desktop now read that same value; `CINEMA_DB_CONNECTION`, when set, overrides it in both processes.

## Server environment for deployment

Set these values in the API deployment environment or secret manager (never commit real values). The session signing key stays on the API server; only a short-lived signed session is sent to the desktop client.

| Variable | Purpose |
| --- | --- |
| `CINEMA_DB_CONNECTION` | SQL Server connection string used by this API and the desktop app |
| `PAYMENTS_CLIENT_KEY` | Random shared client key, at least 32 characters |
| `PAYMENTS_SESSION_SIGNING_KEY` | Separate random server-only key used to sign short-lived cashier sessions; never put this value in the desktop client |
| `PAYOS_CLIENT_ID` | PayOS merchant client ID |
| `PAYOS_API_KEY` | PayOS merchant API key |
| `PAYOS_CHECKSUM_KEY` | PayOS webhook signature key |
| `PAYOS_RETURN_URL` | HTTPS URL PayOS opens after checkout |
| `PAYOS_CANCEL_URL` | HTTPS URL PayOS opens when the payer cancels |
| `ASPNETCORE_URLS` | API listener, normally behind the HTTPS proxy |

Configure the PayOS merchant webhook URL as `https://<public-host>/webhooks/payos`. The API validates the PayOS SDK signature, VND currency, success code, order code, payment link ID and amount before changing an invoice to `Paid`. A hosted worker marks 15-minute pending orders expired and releases their seat/voucher claims.

## WinForms environment for deployment

Set `PAYMENTS_API_URL` to the API base URL (HTTPS in deployment) and set `PAYMENTS_CLIENT_KEY` to the same shared client key. The SQL connection string should point to the same database as the API.

## Database rollout

Back up the database, then run the additive migrations in order:

1. `Database/Migrations/20261014_RolesAndPasswordStorage.sql`
2. `Database/Migrations/20261014_SalesAndPayOsSafety.sql`
3. `Database/Migrations/20261015_SpecializedAdminRoles.sql`
4. `Database/Migrations/20261015_AdminTong.sql`
5. `Database/Migrations/20261022_DataIntegrityAndTypedParameters.sql`
6. `Database/Migrations/20261022_FoodSizesAndImages.sql`

Back up the database before rollout. The last migration preflights legacy values and cross-show seat references, reports incompatible record keys, and rolls back without modifying or deleting historical sales. It also reports invoice seat-count/list differences for manual review. `Database/NHHCinema.sql` is the schema initializer for a new database; it sets both sample logins to require a password change on first login. `Database/ResetDemoDatabase.sql` is destructive and is only for disposable demo data.

The first migration adds roles and password storage, the second adds sales/payment safety tables and columns, the next two finalize admin roles, and the last two add trusted integrity constraints, composite seat-to-invoice/show relationships, food descriptions/images, and size-specific prices. Run all six in this order on an existing database. Sample accounts on an existing database are also marked to require a password change once by the integrity migration.

The API can start and serve `/health` before credentials are available. Payment routes return HTTP 503 until PayOS credentials and return/cancel URLs are configured. No payment can be completed in a local deployment unless PayOS credentials are configured and PayOS can reach the HTTPS webhook. The payment endpoint deliberately fails closed when these settings are missing.
