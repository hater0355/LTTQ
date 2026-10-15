# PayOS Payments API

The WinForms application does not contain PayOS credentials. Run this ASP.NET Core API beside the SQL database and publish it behind an HTTPS reverse proxy with a publicly reachable webhook route at `/webhooks/payos`.

## Server environment

Set these values in the deployment environment or secret manager (never commit real values):

| Variable | Purpose |
| --- | --- |
| `CINEMA_DB_CONNECTION` | SQL Server connection string used by this API and the desktop app |
| `PAYMENTS_CLIENT_KEY` | Random shared client key, at least 32 characters |
| `PAYOS_CLIENT_ID` | PayOS merchant client ID |
| `PAYOS_API_KEY` | PayOS merchant API key |
| `PAYOS_CHECKSUM_KEY` | PayOS webhook signature key |
| `PAYOS_RETURN_URL` | HTTPS URL PayOS opens after checkout |
| `PAYOS_CANCEL_URL` | HTTPS URL PayOS opens when the payer cancels |
| `ASPNETCORE_URLS` | API listener, normally behind the HTTPS proxy |

Configure the PayOS merchant webhook URL as `https://<public-host>/webhooks/payos`. The API validates the PayOS SDK signature, VND currency, success code, order code, payment link ID and amount before changing an invoice to `Paid`. A hosted worker marks 15-minute pending orders expired and releases their seat/voucher claims.

## WinForms environment

Set `PAYMENTS_API_URL` to the API base URL (HTTPS in deployment) and set `PAYMENTS_CLIENT_KEY` to the same shared client key. The SQL connection string should point to the same database as the API.

## Database rollout

Back up the database, then run the additive migrations in order:

1. `Database/Migrations/20261014_RolesAndPasswordStorage.sql`
2. `Database/Migrations/20261014_SalesAndPayOsSafety.sql`
3. `Database/Migrations/20261015_SpecializedAdminRoles.sql`
4. `Database/Migrations/20261015_AdminTong.sql`

These migrations retain existing accounts and invoices. Existing invoices are marked `Paid`; legacy cashier passwords are upgraded to salted PBKDF2 after their next successful login. `Database/NHHCinema.sql` is the non-destructive schema initializer for a new database. `Database/ResetDemoDatabase.sql` is destructive and is only for disposable demo data.

The API can start and serve `/health` before credentials are available. Payment routes return HTTP 503 until PayOS credentials and return/cancel URLs are configured. No payment can be completed in a local deployment unless PayOS credentials are configured and PayOS can reach the HTTPS webhook. The payment endpoint deliberately fails closed when these settings are missing.
