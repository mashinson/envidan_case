# Envidan Case

Solutions for the three assignments in the case. All code targets .NET 9.

| Assignment | Task | Solution | Write-up |
|---|---|---|---|
| 1 | Add Square support to `SOLID.Assignment` and refactor it to follow SOLID | `SOLID/` | [SOLID/SOLID.md](SOLID/SOLID.md) |
| 2 | Name your 3 most used design patterns and briefly explain them | – | [DesignPatterns.md](DesignPatterns.md) |
| 3 | Refactor the `Onion.Assignment` REST API into Onion Architecture | `Onion/` | [Onion/ARCHITECTURE.md](Onion/ARCHITECTURE.md) |

## Assignment 1 – SOLID

Solution: `SOLID/Envidan.sln`, with the console app in `SOLID/SOLID` and unit tests in `SOLID/SOLID.Tests`.

`SOLID.md` explains what changed, how `Square` was added and how each SOLID principle was applied.

```
cd SOLID
dotnet run --project SOLID
dotnet test
```

## Assignment 2 – Design patterns

No code. `DesignPatterns.md` covers the three patterns I use most (Dependency Injection, Chain of Responsibility, Observer) and a few more from my projects (Strategy, Resolver/Registry, Facade).

## Assignment 3 – Onion Architecture

Solution: `Onion/Onion.sln`, split into `Onion.Domain`, `Onion.Application` and `Onion.Assignment` (the API), with tests in `Onion/tests`.

`ARCHITECTURE.md` explains the layers, the request flow, the decisions and what I would add as the project grows.

```
cd Onion
dotnet run --project Onion.Assignment   # Swagger UI at /swagger
dotnet test
```
