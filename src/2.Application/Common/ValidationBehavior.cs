using Domain.Shared.Responses;
using FluentValidation;
using MediatR;

namespace Application.Common
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));

            var failures = validationFailures.SelectMany(x => x.Errors).Where(x => x != null).ToList();

            if (failures.Any())
            {
                var response = Activator.CreateInstance<TResponse>();

                foreach (var failure in failures)
                {
                    response.AddError(failure.ErrorMessage);
                }

                return response;
            }

            return await next();
        }
    }
}
