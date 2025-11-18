using Domain.Shared.Enums;
using Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.API.Interfaces;

namespace Service.API.Controllers
{
    public abstract class SystemController : ControllerBase
    {
        private readonly IErrorMessageProvider _errorMessages;
        protected IMediator _mediator;

        protected SystemController(IMediator mediator, IErrorMessageProvider errorMessages)
        {
            _mediator = mediator;
            _errorMessages = errorMessages;
        }

        protected ActionResult GetActionResult<TResponse>(TResponse response) where TResponse : Response
        {
            if (response == null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { errors = new[] { "Response is null." } });

            if (response.Errors.Any())
            {
                var allErrors = GetAllResponseErrors(response.Errors);

                // 400 por padrão
                return StatusCode(StatusCodes.Status400BadRequest, new { errors = allErrors });
            }

            // Sucesso: deixa seu fluxo como já estava
            var method = HttpContext.Request.Method.ToUpper();
            if (method == "POST") return StatusCode(response.StatusCode ?? StatusCodes.Status201Created, response);
            if (method is "PUT" or "PATCH") return Ok(response);
            if (method == "DELETE") return NoContent();
            return Ok(response);
        }

        private List<string> GetAllResponseErrors(IReadOnlyList<string> errors)
        {
            var allErrors = new List<string>();
            foreach (var error in errors)
            {
                if (Enum.TryParse<EErrorCode>(error, out var errorCode))
                {
                    var errorMessage = _errorMessages.Get(errorCode);
                    if (!string.IsNullOrEmpty(errorMessage) && !allErrors.Contains(errorMessage))
                        allErrors.Add(errorMessage);
                }
                else
                {
                    allErrors.Add(error);
                }
            }

            allErrors = allErrors.Distinct().ToList();

            return allErrors;
        }

        private bool HasPermission(string permission)
        {
            // Implement your permission logic here
            // For example, check if the user has the required permission
            return true; // Placeholder for actual permission check
        }
    }
}