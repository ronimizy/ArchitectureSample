using Application.Dto;
using MediatR;

namespace Application.Contracts.TransferOrders;

public static class CreateTransferOrder
{
    public record struct Command : IRequest<Response>;

    public record struct Response(TransferOrderDto Order);
}