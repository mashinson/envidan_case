using Onion.Application.DTOs.Requests;
using FluentValidation;
using Onion.Application.Services;
using Onion.Application.Validators;

namespace Onion.Application.Tests;

public class GreetingServiceTests
{
    private readonly GreetingService _greetingService = new(new GetGreetingRequestValidator());

    [Fact]
    public async Task GreetAsync_ValidName_ReturnsGreeting()
    {
        var response = await _greetingService.GreetAsync(new GetGreetingRequest("Bob"));

        Assert.Equal("Hello, Bob!", response.Message);
    }

    [Fact]
    public async Task GreetAsync_InvalidName_ThrowsValidationExceptionWithNameError()
    {
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _greetingService.GreetAsync(new GetGreetingRequest("")));

        Assert.Contains(exception.Errors, error => error.PropertyName == nameof(GetGreetingRequest.Name));
    }
}
