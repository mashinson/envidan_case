# Architecture

The original `Onion.Assignment` API, refactored into onion layers. It has one endpoint: `GET /hello?name=Bob` returns `Hello, Bob!` as plain text.

## Structure

Three projects and one test project:

    Onion.sln
    Onion.Domain
        Entities/Greeting.cs
        Helpers/GreetingBuilder.cs
    Onion.Application
        DTOs/ (request and response)
        Interfaces/IGreetingService.cs
        Services/GreetingService.cs
        Validators/GetGreetingRequestValidator.cs
        DependencyInjection.cs
    Onion.Assignment (the API)
        Endpoints/GreetingEndpoints.cs
        Middleware/GlobalExceptionHandler.cs
        Extensions/SwaggerExtensions.cs
        Extensions/EndpointExtensions.cs
        Program.cs
    tests/Onion.Application.Tests

References go one way: Assignment -> Application -> Domain. Domain references nothing and has no packages.

- Domain: the `Greeting` model (data only) and `GreetingBuilder`, which builds the "Hello, name!" text.
- Application: `GreetingService`, the DTOs and the validation rules. It knows nothing about HTTP.
- Assignment: the API. Endpoint, error handling, Swagger and DI setup. No business rules.

## How a request flows

1. The `/hello` endpoint creates a `GetGreetingRequest` from the query string and calls `IGreetingService`.
2. `GreetingService` validates it with `ValidateAndThrowAsync`.
3. If the name is invalid, FluentValidation throws a `ValidationException`. Otherwise `GreetingBuilder` builds the greeting.
4. `GlobalExceptionHandler` turns any exception into a `ProblemDetails` response.

| Case | Status | Body |
| --- | --- | --- |
| Valid name | 200 | `Hello, {name}!` (text/plain) |
| Missing, blank or longer than 100 characters | 400 | validation problem with an `errors` entry for `Name` |
| Any other exception | 500 | generic message, no exception details |

## Decisions

- No Infrastructure project. Onion solutions usually have one, but there is nothing to put in it here: no database, no external services, no files. It would only contain an empty `AddInfrastructure()`. It gets added with the first data access, and repositories come with it.
- Validation in one place. The rules live in one FluentValidation validator in Application, so they can be tested without HTTP and the endpoint has no `if` checks. `request.Name!` in the service is safe because the validator already rejects null.
- One global exception handler (`IExceptionHandler` + `ProblemDetails`) instead of try/catch in each endpoint. New endpoints get the same error format without extra code.
- FluentValidation's `ValidationException` instead of a custom one. The handler maps it straight to a 400.
- No logic in models. `Greeting` is plain data, and `GreetingBuilder` builds the text.
- DI per layer. `AddApplication()` registers the service and the validators, which keeps `Program.cs` short.
- Minimal API, like the original template, to keep the diff small. Endpoints live in `GreetingEndpoints` and are registered through `MapEndpoints()`, so `Program.cs` doesn't change when new ones are added.
- Swashbuckle only. The template had both `Microsoft.AspNetCore.OpenApi` and Swashbuckle, which do the same job. I kept Swashbuckle because it includes the Swagger UI and was added to the template manually.

## Libraries

- FluentValidation 12.1.1, with its DI extensions: request validation
- Swashbuckle.AspNetCore 7.2.0: Swagger UI and API docs
- xUnit 2.9.2, Microsoft.NET.Test.Sdk, coverlet: tests
- Microsoft.Extensions.DependencyInjection.Abstractions 9.0.0: needed for `AddApplication()`

`ProblemDetails` and `IExceptionHandler` come with ASP.NET Core, so error handling needs no extra package.

## Left out

- Custom logging. ASP.NET Core's default logging is used. On .NET 9 it also logs handled exceptions, including validation failures, as errors.
- API and Domain tests. The logic is in the validator and the service, so that is what's tested.

## If the project grows

Not built here, but this is what I would use:

- Data access: an `Onion.Infrastructure` project with EF Core. Repository interfaces go in Domain, so the inner layers don't depend on the database.
- Database rules: EF Core Fluent API, one configuration class per table, for required fields, lengths, foreign keys and unique indexes, plus migrations. FluentValidation checks the input first, and the database catches whatever gets past it.
- Mapping: Mapperly or manual mapping between domain models and DTOs, so the API never returns domain models directly.
- MediatR: only once there are many handlers. For one use case a service is enough. Newer versions also need a paid license for larger companies.
- Tests: `WebApplicationFactory` for API tests, plus integration tests against a real test database.
- Docker: a `Dockerfile` in `Onion.Assignment` and a `docker-compose.yml` in the solution root to run the API with its database.
- Azure: Bicep (or Terraform) in an `infra/` folder, and an Azure DevOps or GitHub Actions pipeline that builds the image, pushes it to Azure Container Registry and deploys it to App Service or Container Apps. These aren't code projects, so nothing references them.

## Tests

`tests/Onion.Application.Tests` covers the validator (null, empty, blank, 100 characters passes, 101 fails) and `GreetingService` (valid name, invalid name throws). There are no mocks, because the service only depends on the validator.

Test projects go in `tests/` and are named after the project they test. Test names follow `Method_Condition_Result`.

Next tests to add:

- Domain tests for `GreetingBuilder`, once it has real logic.
- API tests with `WebApplicationFactory` for 200, 400 and 500 and the error body. For 500, swap in a service that throws.
- Infrastructure tests against a real test database, not the in-memory provider.
- Mocks only at external boundaries, such as other services.

## Run

```
dotnet run --project Onion.Assignment
dotnet test
```

Swagger UI: http://localhost:5242/swagger. Example call: http://localhost:5242/hello?name=Bob returns `Hello, Bob!`.
