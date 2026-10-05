using Onion.Domain.Entities;

namespace Onion.Domain.Helpers;

public static class GreetingBuilder
{
    public static Greeting Build(string name) => new()
    {
        Message = $"Hello, {name}!" 
    };
}