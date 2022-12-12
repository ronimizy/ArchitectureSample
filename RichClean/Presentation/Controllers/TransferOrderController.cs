using Application.Contracts.TransferOrders;
using Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.TransferOrders;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferOrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransferOrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<TransferOrderDto>> CreateAsync()
    {
        var command = new CreateTransferOrder.Command();
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Order);
    }

    [HttpPost("{orderId:guid}/operations")]
    public async Task<ActionResult<TransferOperationDto>> AddTransferOperationAsync(
        Guid orderId,
        [FromBody] AddTransferOperationModel model)
    {
        var command = new AddTransferOperation.Command(orderId, model.StudentId, model.StudentGroupId);
        var response = await _mediator.Send(command, CancellationToken);

        return Ok(response.Operation);
    }

    [HttpPost("{orderId:guid}/execute")]
    public async Task<ActionResult> ExecuteOrderAsync(Guid orderId)
    {
        var command = new ExecuteTransferOrder.Command(orderId);
        await _mediator.Send(command, CancellationToken);

        return Ok();
    }
}