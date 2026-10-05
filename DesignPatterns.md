# Assignment 2 – Design patterns

## Patterns I use most

### Dependency Injection
A class gets its dependencies from outside instead of creating them itself. Classes stay small and don't depend on each other's details, and each part can be replaced or tested on its own.

**Example:** in the Onion task, `GreetingService` needs a name validator. It receives one through its constructor, and `AddApplication()` registers which implementation the app uses.

### Chain of Responsibility
A request goes through a series of handlers. Each one does a single job, then passes the request on or stops it. No handler needs to know how the others work.

**Example:** the ASP.NET Core middleware pipeline set up in `Program.cs`. In the Onion task the order is exception handling, Swagger, HTTPS redirection and then the endpoint. The exception handler comes first, so a failure anywhere after it comes back as the same kind of error response.

### Observer
A publisher announces that something happened, and every subscriber gets notified. The publisher doesn't know who is listening, so subscribers can come and go without changing it, and nobody has to keep polling for updates.

- **Inside one application:** C# has this built in with events and delegates. Subscribers attach a handler with `+=`, and each handler runs when the event is raised.
- **Between services:** a message broker such as RabbitMQ or Azure Service Bus does the same job. An "order created" message, for example, can be handled separately by an email service and a stock service, and the sender knows about neither.

**Example:** in a Windows background service I built, the API pushes new tasks to the service over SignalR. The service handles them as they arrive instead of checking for work over and over.

## Interesting cases

### Strategy
The same task can be done in different ways, and the right one is picked at runtime.

**Example:** while migrating an automotive retail platform to .NET Core, I designed the inventory CSV export for car manufacturers (OEMs). Every OEM wants a different file, with its own columns, formats and filters. The shared steps (validation, file creation, publishing) are written once, and each OEM gets a strategy with its own rules.

### Resolver / Registry (Factory)
A component that takes a key, such as a name, and returns the implementation that matches it.

**Example:** in the same export, `OemFeedExecutionStrategyResolver` takes the configured feed name and returns its strategy. The main pipeline never mentions a specific OEM, so adding a new one only means adding a new strategy.

### Facade
One simple entry point in front of a more complicated part of the system. Callers make one call and don't need to know what happens behind it.

**Example:** I built a Windows background service to replace part of a legacy desktop application. To open and save Ranorex, JMeter and SoapUI projects, I reused code from the old application but put it behind a facade. The new service only talks to the facade and never touches the legacy code directly. The old code stays out of the new design, and it can be rewritten or replaced later without changing the rest of the service.
