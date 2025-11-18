using Domain.Shared.Requests;
using Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace Application.Common
{
    public class RequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : Request<TResponse>
        where TResponse : Response
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var endpoint = httpContext?.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null || endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;

            httpContext.Request.Headers.ToList().ForEach(x =>
            {
                Console.WriteLine($"Chave: {x.Key} | Valor: {x.Value}");
            });

            if (!IPAddress.TryParse(httpContext?.Request?.Headers["X-Real-IP"] ?? httpContext?.Request?.Headers["X-Forwarded-For"], out IPAddress ipAddress))
            {
                ipAddress = IPAddress.None;
            }

            request.SetIpAddr(ipAddress);

            if (allowAnonymous) return await next();

            var idMethods = new string[] { "PUT", "DELETE", "PATCH", "GET" };
            if (idMethods.Contains(httpContext.Request.Method.ToUpperInvariant()))
            {
                var id = httpContext.Request.RouteValues["id"]?.ToString();

                if (Guid.TryParse(id, out var guid)) request.SetId(guid);
            }

            var subStr = httpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? httpContext.User.FindFirst("userId")?.Value;

            var pidStr = httpContext!.User.FindFirst("pid")?.Value ?? httpContext.User.FindFirst("userId")?.Value;

            var tidStr = httpContext!.User.FindFirst("tid")?.Value;

            var cidStr = httpContext!.User.FindFirst("cid")?.Value;

            var uidStr = httpContext!.User.FindFirst("uid")?.Value;

            if (Guid.TryParse(subStr, out var userId)) request.SetUserId(userId);

            if (Guid.TryParse(pidStr, out var personId)) request.SetPersonId(personId);

            if (Guid.TryParse(tidStr, out var tenantId)) request.SetTenantId(tenantId);

            if (Guid.TryParse(cidStr, out var companyId)) request.SetCompanyId(companyId);

            if (Guid.TryParse(uidStr, out var unitId)) request.SetUnitId(unitId);

            return await next();
        }
    }
}
