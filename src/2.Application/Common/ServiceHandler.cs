using Domain.Shared.Responses;
using MediatR;

namespace Application.Common
{
    public abstract class ServiceHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        protected TResponse _response;

        protected ServiceHandler()
        {
            _response = Activator.CreateInstance<TResponse>();
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
