# Database Configuration Guide

## Overview
This project supports three database connection scenarios:

### 1. Local Development (Docker Compose)
Uses a local PostgreSQL container.

**Setup:**
```bash
docker-compose up -d
```

**Connection String** (already configured in `appsettings.json`):
```
Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword
```

---

### 2. Local Development (Remote Database)
Connect to the production Supabase database from your local machine for testing.

**Environment Variable:**
```
ConnectionStrings:DefaultConnection=User Id=postgres.eonqesqusvndztotafju;Password=YOUR_PASSWORD;Server=aws-1-eu-north-1.pooler.supabase.com;Port=5432;Database=postgres
```

**Important:** Use the **session pooler** endpoint for this connection.

---

### 3. Azure Production
Connect from Azure App Service to the Supabase production database.

**Environment Variable:**
```
ConnectionStrings:DefaultConnection=Host=aws-1-eu-north-1.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.eonqesqusvndztotafju;Password=YOUR_PASSWORD;SSL Mode=Require;Trust Server Certificate=true;
```

**Important:**
- Use the **session pooler** endpoint
- SSL Mode is required (`Require`)
- Trust Server Certificate is enabled

---

## Connection Methods

| Environment | Method | Connection Endpoint |
|---|---|---|
| Local Dev | Docker Compose | `localhost:5432` |
| Local Dev | Remote DB | `aws-1-eu-north-1.pooler.supabase.com` (session pooler) |
| Azure Prod | Remote DB | `aws-1-eu-north-1.pooler.supabase.com` (session pooler) |

---

## Troubleshooting

- Check "Log stream" in app service on azure. 