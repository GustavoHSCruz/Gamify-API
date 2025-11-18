using Domain.Shared.Responses;

namespace Domain.Shared.Requests
{
    public class CommandRequest<TResponse> : Request<TResponse> where TResponse : CommandResponse
    {
    }
}
