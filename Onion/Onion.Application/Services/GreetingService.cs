using FluentValidation;
using Onion.Application.DTOs.Requests;
using Onion.Application.DTOs.Responses;
using Onion.Application.Interfaces;
using Onion.Domain.Helpers;

namespace Onion.Application.Services;

public class GreetingService(IValidator<GetGreetingRequest> validator) : IGreetingService
{
    public async Task<GreetingResponse> GreetAsync(GetGreetingRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var greeting = GreetingBuilder.Build(request.Name!);

        return new GreetingResponse(greeting.Message);
    }
}