using Domain.Shared.Interfaces;
using Domain.Shared.Requests;
using Domain.Shared.Responses;

namespace Application.Common
{
    public abstract class Query<TRequest, TResponse> : ServiceHandler<TRequest, TResponse>
        where TRequest : QueryRequest<TResponse>
        where TResponse : Response
    {
        protected readonly IReadRepository _repository;
        protected int Take { get; private set; }
        protected int Skip { get; private set; }

        protected Query(IReadRepository repository)
        {
            _repository = repository;
            _response = Activator.CreateInstance<TResponse>();
        }

        protected abstract Task<TResponse> GetData(TRequest request);

        public override async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Take = request.GetItems() == 0 ? 10 : request.GetItems();
            Skip = request.GetPages();

            _response = await GetData(request);

            if (_response.Errors.Any()) return _response;

            return _response;
        }
    }
}
