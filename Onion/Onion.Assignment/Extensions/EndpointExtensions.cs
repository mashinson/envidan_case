using Onion.Assignment.Endpoints;

namespace Onion.Assignment.Extensions;

public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGreetingEndpoints();

        return app;
    }
}
