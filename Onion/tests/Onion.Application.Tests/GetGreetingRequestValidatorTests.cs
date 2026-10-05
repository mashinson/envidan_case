using FluentValidation.TestHelper;
using Onion.Application.DTOs.Requests;
using Onion.Application.Validators;

namespace Onion.Application.Tests;

public class GetGreetingRequestValidatorTests
{
    private readonly GetGreetingRequestValidator _validator = new();

    [Fact]
    public void Validate_ValidName_HasNoErrors()
    {
        var result = _validator.TestValidate(new GetGreetingRequest("Bob"));

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_MissingName_HasErrorForName(string? name)
    {
        var result = _validator.TestValidate(new GetGreetingRequest(name));

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Validate_NameAtMaxLength_HasNoErrors()
    {
        var result = _validator.TestValidate(new GetGreetingRequest(new string('a', 100)));

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_NameOverMaxLength_HasErrorForName()
    {
        var result = _validator.TestValidate(new GetGreetingRequest(new string('a', 101)));

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
