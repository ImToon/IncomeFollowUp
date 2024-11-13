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
