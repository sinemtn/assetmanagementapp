# Backend Docker

This backend can be containerized directly from the `Backend` folder.

## Build the image

```bash
docker build -t assetmanagement-backend .
```

## Run the container

```bash
docker run --rm -p 5000:5000 \
	-e PORT=5000 \
	-e ASPNETCORE_ENVIRONMENT=Production \
	assetmanagement-backend
```

The API listens on port `5000` inside the container because [Server/Server.cs](c:\Users\ano\OneDrive - Sysmex Asia Pacific Pte Ltd\Repositories\assetmanagementapp\Backend\Server\Server.cs) reads the `PORT` environment variable.

## Run with Docker Compose

```bash
docker compose up --build
```

This starts both services:

- `backend`: the ASP.NET API on port `5000`
- `db`: PostgreSQL 16 on port `5432`

The PostgreSQL container automatically loads the schema dump from [INSTRUCTION/database-schema.md](c:\Users\ano\OneDrive - Sysmex Asia Pacific Pte Ltd\Repositories\assetmanagementapp\INSTRUCTION\database-schema.md) on the first startup of a fresh volume by mounting it as an init `.sql` file.

## Environment overrides

The container uses ASP.NET configuration binding, so nested settings can be overridden with environment variables.

Examples:

```bash
PORT=5000
ASPNETCORE_ENVIRONMENT=Production
POSTGRES_DB=assetmanagement_db
POSTGRES_USER=admin
POSTGRES_PASSWORD=Admin12345;
```

If you want to keep values outside source control, copy `.env.example` to `.env` and adjust the values there before running `docker compose up --build`.

## Reset the local database

The schema import only runs when PostgreSQL initializes an empty data directory. If you want to reload the schema from scratch, remove the database volume and start again.

```bash
docker compose down -v
docker compose up --build
```
