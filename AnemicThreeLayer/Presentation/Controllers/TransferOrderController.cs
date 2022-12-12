using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.TransferOrders;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferOrderController : ControllerBase
{
    private readonly ITransferOrderService _service;

    public TransferOrderController(ITransferOrderService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost]
    public async Task<ActionResult<TransferOrderDto>> CreateAsync()
    {
        var order = await _service.CreateTransferOrderAsync(CancellationToken);
        return Ok(order);
    }

    [HttpPost("{orderId:guid}/operations")]
    public async Task<ActionResult<TransferOperationDto>> AddTransferOperationAsync(
        Guid orderId,
        [FromBody] AddTransferOperationModel model)
    {
        var operation = await _service.AddTransferOperationAsync
        (
            orderId,
            model.StudentId,
            model.StudentGroupId,
            CancellationToken
        );

        return Ok(operation);
    }

    [HttpPost("{orderId:guid}/execute")]
    public async Task<ActionResult> ExecuteOrderAsync(Guid orderId)
    {
        await _service.ExecuteOrderAsync(orderId, CancellationToken);
        return Ok();
    }
}