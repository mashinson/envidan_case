using FluentValidation;
using Onion.Application.DTOs.Requests;

namespace Onion.Application.Validators;

public class GetGreetingRequestValidator : AbstractValidator<GetGreetingRequest>
{
    private const int MaxNameLength = 100;

    public GetGreetingRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(MaxNameLength);
    }
}