using Onion.Application.DTOs.Requests;
using Onion.Application.Interfaces;

namespace Onion.Assignment.Endpoints;

public static class GreetingEndpoints
{
    public static IEndpointRouteBuilder MapGreetingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/hello", async (string? name, IGreetingService greetingService, CancellationToken cancellationToken) =>
            {
                var response = await greetingService.GreetAsync(new GetGreetingRequest(name), cancellationToken);

                return response.Message;
            })
            .Produces<string>(StatusCodes.Status200OK, "text/plain")
            .ProducesValidationProblem();

        return app;
    }
}
