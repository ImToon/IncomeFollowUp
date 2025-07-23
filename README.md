# Freelance Income Monitor

This project is a **.NET-based web application** designed to help freelancers monitor their monthly income. It consists of two separate applications:

1. **REST API** - Backend service built with .NET 8, providing endpoints for managing user settings, workdays, and income statistics.
2. **Blazor Web Application** - Frontend built with Blazor, providing an interactive UI to manage workdays, view monthly income, and visualize past earnings.

## Features

Coming soon

## Tech Stack

- **Backend**: .NET 8 REST API.
- **Frontend**: Blazor WebAssembly.
- **Database**: MySql.
- **Docker**: Both API (with database) and web app are containerized using Docker and managed through Docker Compose.

## Setup Instructions

1. Clone the repository.
2. Ensure Docker and Docker Compose are installed.
3. Run the following command to build and start the containers:

```bash
docker-compose up --build
```

This will start:

- REST API
- Blazor Web App
- MySql Server

If you want to seed your database for the Settings and your Monthly outcomes, You can add these 2 files inside a Seed folder in the Infrastructure project :

1. `Seed/Settings.json`

```json
{
  "Id": "b094e16e-6b15-4ab9-9952-f66b90e35ef3",
  "DailyRate": 1,
  "ExpectedMonthlyIncome": 1
}
```

2. `Seed/MonthlyOutcomes.json`

```json
[
  {
    "Id": "EEB763CF-50A3-4F98-9D8E-A76183125A8E",
    "Name": "Car",
    "Amount": 1
  },
  ...
]

```
