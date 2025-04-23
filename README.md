# Mobile Billing API (SE4458 Midterm Project)

This project simulates a RESTful billing system for a mobile service provider.  
It includes usage tracking (phone/internet), bill calculation, partial payments, and detailed reports.

## Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- MySQL (Azure Database for MySQL)
- Azure App Service (API hosting)
- Swagger (OpenAPI)
- JWT Authentication

## Live Deployment

- API Base URL: [https://mobilebillingapi-efe.azurewebsites.net](https://mobilebillingapi-efe.azurewebsites.net)
- Swagger UI: [https://mobilebillingapi-efe.azurewebsites.net/swagger](https://mobilebillingapiefe-hug0ahbxddd8dtdh.canadacentral-01.azurewebsites.net/swagger/index.html)
- Youtube: [Click Here](https://www.youtube.com/watch?v=qvJeY2P4M2w)

## Default Test Credentials

```json
{
  "username": "efe",
  "password": "1234"
}
```

Login via `/api/v1/Auth/login` to receive a JWT token. Use the token for authorizing other endpoints.

## API Features

- `POST /api/v1/Auth/login` – Login with static credentials to receive a JWT
- `POST /api/v1/Usage` – Add phone or internet usage
- `POST /api/v1/Bill/calculate` – Calculates monthly bill:
  - Phone: First 1000 minutes free, 10$ per additional 1000 minutes
  - Internet: 50$ for up to 20GB (20480MB), then 10$ for each extra 10GB
- `POST /api/v1/Bill/pay` – Pays a bill (supports **partial payments** and keeps remaining amount)
- `GET /api/v1/Bill/{subscriberId}` – Get summary of all bills for a subscriber
- `GET /api/v1/Bill/detailed?...` – Get detailed breakdown with pagination
- Swagger UI for documentation and testing
- Versioned endpoints: `/api/v1/...`
- JWT-secured endpoints
- Hosted on Azure App Service
- Data stored on Azure Database for MySQL

## Test Note for Instructor

> Please **do not test using subscriber IDs 1, 2, 5, or 6** — these are reserved for example/demo data.  
> You may use any other subscriber ID (e.g. 3, 4, 7, etc.) to test the full billing flow.

## Database Schema Overview

### Tables

- `Usages`  
  - `SubscriberId`, `UsageType` (`Phone` / `Internet`), `Month`, `Year`, `Amount`
- `Bills`  
  - `SubscriberId`, `Month`, `Year`, `TotalBill`, `IsPaid`

Each monthly bill is generated from usage, and partial payments reduce `TotalBill`. Once the bill is fully paid, `IsPaid` is set to true.

## Local Setup Instructions

1. Clone the repository.
2. Update `appsettings.json` with your own MySQL connection string:

```json
 "ConnectionStrings": {
   "DefaultConnection": "Server=mobilebilling-mysql.mysql.database.azure.com; Port=3306; Database=mobilebillingdb; Uid=efeadmin; Pwd=Root1234; SslMode=Preferred;"
 },
```


3. Run the project:

```bash
dotnet ef database update
dotnet run
```

4. Navigate to `http://localhost:<port>/swagger` to test locally.

## Project Requirements Checklist

- [x] RESTful API, no front-end required
- [x] Clean service-layer structure, no direct DB calls in controllers
- [x] API versioning supported (e.g. `/api/v1/...`)
- [x] Swagger/OpenAPI documentation included
- [x] JWT authentication implemented
- [x] Paging supported in bill details
- [x] Hosted on Azure App Service
- [x] MySQL used via Azure (no in-memory DB)

## ER Diagram
![image](https://github.com/user-attachments/assets/8cb0ce28-ea93-40c4-af88-cf46654f9b77)



## Author

- Efe Demirtaş  20070001053
- Yaşar University – Computer Engineering  
- SE4458  
- Spring 2025 – Midterm Project

---

> This README reflects all common and task-specific requirements of the SE4458 Midterm assignment.
