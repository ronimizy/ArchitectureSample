using Application.Abstractions.DataAccess;
using Application.Mapping;
using Domain.Transfers;
using MediatR;
using static Application.Contracts.TransferOrders.CreateTransferOrder;

namespace Application.Transfers;

internal class CreateTransferOrderHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateTransferOrderHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var order = new TransferOrder(Guid.NewGuid());
        
        _context.TransferOrders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(order.AsDto());
    }
}