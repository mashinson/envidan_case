using Onion.Application.DTOs.Requests;
using Onion.Application.DTOs.Responses;

namespace Onion.Application.Interfaces;

public interface IGreetingService
{
    Task<GreetingResponse> GreetAsync(GetGreetingRequest request, CancellationToken cancellationToken = default);
}