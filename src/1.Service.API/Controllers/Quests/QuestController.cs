using Application.Commands.Quests;
using Domain.Core.Requests.Quests;
using Domain.Core.Responses.Quests;
using Domain.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.API.Interfaces;

namespace Service.API.Controllers.Quests;

[ApiController]
[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class QuestController : SystemController
{
    public QuestController(IMediator mediator, IErrorMessageProvider errorMessages) : base(mediator, errorMessages)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateQuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateQuestRequest request, CancellationToken cancellationToken)
    {
        return GetActionResult(await _mediator.Send(request, cancellationToken));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateQuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateQuestRequest request, CancellationToken cancellationToken)
    {
        return GetActionResult(await _mediator.Send(request, cancellationToken));
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetQuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] GetQuestRequest request, CancellationToken cancellationToken)
    {
        return GetActionResult(await _mediator.Send(request, cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetByIdQuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetByIdResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromQuery] GetByIdQuestRequest request, CancellationToken cancellationToken)
    {
        return GetActionResult(await _mediator.Send(request, cancellationToken));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(DeleteQuestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromBody] DeleteQuestRequest request, CancellationToken cancellationToken)
    {
        return GetActionResult(await _mediator.Send(request, cancellationToken));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch([FromBody] PatchQuestRequest request, CancellationToken cancellationToken)
    {
        return GetActionResult(await _mediator.Send(request, cancellationToken));
    }
}