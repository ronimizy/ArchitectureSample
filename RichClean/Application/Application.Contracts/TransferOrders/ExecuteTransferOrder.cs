using MediatR;

namespace Application.Contracts.TransferOrders;

public static class ExecuteTransferOrder
{
    public record struct Command(Guid OrderId) : IRequest;
}