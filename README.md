# IdentityApi

An ASP.NET Core Web API for user registration and login built on ASP.NET Core Identity and JWT, with an Angular 17 client app (work in progress) in `ClientApp`.

## Tech Stack

**API (`Api`)**

- .NET 8 (`net8.0`)
- ASP.NET Core Web API
- ASP.NET Core Identity with Entity Framework Core 8 and SQL Server
- JWT bearer authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`, `System.IdentityModel.Tokens.Jwt`)
- Swashbuckle (Swagger UI)

**Client (`ClientApp`)**

- Angular 17 (NgModule-based) with Angular SSR (Express)
- Bootstrap 5 and ng-bootstrap

## Project Structure

| Path | Description |
| --- | --- |
| `Api/Controllers` | `AccountController` with the register, login and token refresh endpoints. |
| `Api/Data` | `DataContext` (`IdentityDbContext<Users>`) and EF Core migrations. |
| `Api/Models` | `Users` (extends `IdentityUser` with first name, last name and creation date) and the account DTOs. |
| `Api/Services` | `JwtService`, which creates signed JWTs with the user's id, email, first name and last name. |
| `ClientApp` | Angular app with a navbar, footer, home and play pages, a not-found page and a lazy-loaded `account` module (login and register pages). The pages are placeholders and do not call the API yet. |

## Features

- Register with first name, last name, email and password; the email is used as the username and must be unique.
- Log in with username (email) and password; the response contains the user's name and a JWT.
- Refresh the JWT for the signed-in user (requires a valid token).
- Identity password rules: minimum 6 characters, no other complexity requirements.
- Token lifetime configured with `Jwt:ExpiresInDays`.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB, Express or full edition)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`
- Node.js 18+ and npm, plus the Angular CLI (`npm install -g @angular/cli`) for the client

### API configuration

Settings are in `Api/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=AuthDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
},
"Jwt": {
  "Key": "CHANGE_ME_use_user_secrets_or_env_var_min_32_chars",
  "Issuer": "http://localhost:5046",
  "ExpiresInDays": 14
}
```

`Jwt:Key` is only a placeholder. Set your own signing key (at least 32 characters) with user secrets or an environment variable:

```bash
cd Api
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "<a random string of at least 32 characters>"
```

or

```bash
# Linux/macOS
export Jwt__Key="<a random string of at least 32 characters>"
# Windows PowerShell
$env:Jwt__Key="<a random string of at least 32 characters>"
```

### Create the database

```bash
cd Api
dotnet ef database update
```

### Run the API

```bash
cd Api
dotnet run
```

The API listens on `http://localhost:5046`; Swagger UI is at `http://localhost:5046/swagger`.

### Run the client

```bash
cd ClientApp
npm install
ng serve
```

Then open `http://localhost:4200`.

## API Endpoints

| Method | Route | Auth | Description |
| --- | --- | --- | --- |
| POST | `/api/Account/register` | No | Register a user (`firstName`, `lastName`, `email`, `password`). Returns the user's name and a JWT. |
| POST | `/api/Account/login` | No | Log in (`userName`, `password`). Returns the user's name and a JWT. |
| POST | `/api/Account/refresh-user-token` | Bearer JWT | Returns a new JWT for the current user. |

## License

This project is licensed under the MIT License. See [LICENSE.txt](LICENSE.txt).
