using Application.Dto;
using MediatR;

namespace Application.Contracts.TransferOrders;

public static class AddTransferOperation
{
    public record struct Command(Guid OrderId, Guid StudentId, Guid GroupId) : IRequest<Response>;

    public record struct Response(TransferOperationDto Operation);
}