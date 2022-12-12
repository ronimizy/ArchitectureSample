using Application.Dto;

namespace Application.Services;

public interface ITransferOrderService
{
    Task<TransferOrderDto> CreateTransferOrderAsync(CancellationToken cancellationToken);

    Task<TransferOperationDto> AddTransferOperationAsync(
        Guid orderId,
        Guid studentId,
        Guid studentGroupId,
        CancellationToken cancellationToken);

    Task ExecuteOrderAsync(Guid orderId, CancellationToken cancellationToken);
}