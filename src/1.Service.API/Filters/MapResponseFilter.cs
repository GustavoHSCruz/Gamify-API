// WebApi/Filters/MapResponseFilter.cs
using Domain.Shared.Enums;
using Domain.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.API.Interfaces;

namespace Service.API.Filters
{
    public sealed class MapResponseFilter : IResultFilter
    {
        private readonly IErrorMessageProvider _messagesProvider;

        public MapResponseFilter(IErrorMessageProvider messagesProvider) => _messagesProvider = messagesProvider;

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is not ObjectResult obj) return;
            if (obj.Value is not Response response) return;
            if (!response.Errors.Any()) return; // só trata ERRO

            var list = new List<object>();

            // 1) erros por código (enum)
            if (response.Errors.Any())
            {
                list.AddRange(response.Errors.Select(code =>
                {
                    if (Enum.TryParse<EErrorCode>(code, out var errorCode))
                    {
                        return new
                        {
                            code,
                            message = _messagesProvider.Get(errorCode)
                        };
                    }

                    return new
                    {
                        code,
                        message = code
                    };
                }));
            }

            // 2) erros por mensagem (FluentValidation etc.)
            if (response.Errors.Any())
            {
                list.AddRange(response.Errors.Select(msg => new
                {
                    message = msg
                }));
            }

            var problem = new ProblemDetails
            {
                Status = obj.StatusCode ?? StatusCodes.Status400BadRequest,
                Type = "about:blank"
            };
            problem.Extensions["errors"] = list.ToArray();

            if (response.Id is Guid id) problem.Extensions["id"] = id;

            context.Result = new ObjectResult(problem)
            {
                StatusCode = problem.Status,
                DeclaredType = typeof(ProblemDetails)
            };
        }

        public void OnResultExecuted(ResultExecutedContext context) { }
    }
}
