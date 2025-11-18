using Domain.Shared.Requests;
using Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Common
{
    public class QueryRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : QueryRequest<TResponse>
        where TResponse : GetResponse
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryRequestBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext.Request.Method.ToUpper() == "GET")
            {
                if (httpContext.Request.Query.ContainsKey("page"))
                {
                    int page = 1;
                    var items = 10;

                    if (httpContext.Request.Query.ContainsKey("page"))
                    {
                        var pageNumberStr = httpContext.Request.Query["page"].ToString();
                        if (int.TryParse(pageNumberStr, out var pageNumber))
                        {
                            page = pageNumber;
                        }
                    }

                    if (httpContext.Request.Query.ContainsKey("items"))
                    {
                        var itemsPerPageStr = httpContext.Request.Query["items"].ToString();
                        if (int.TryParse(itemsPerPageStr, out var itemsNumber))
                        {
                            items = itemsNumber;
                        }
                    }

                    request.SetPageItems(page, items);
                }

                if (httpContext.Request.Query.ContainsKey("search") && !httpContext.Request.RouteValues.ContainsKey("id"))
                {
                    var search = httpContext.Request.Query["search"].ToString();
                    request.SetSearch(search);
                }

                if (httpContext.Request.Query.ContainsKey("isActive"))
                {
                    var isActiveStr = httpContext.Request.Query["isActive"].ToString();
                    if (bool.TryParse(isActiveStr, out var isActive))
                    {
                        request.SetIsActive(isActive);
                    }
                }
            }

            return await next();
        }
    }
}
