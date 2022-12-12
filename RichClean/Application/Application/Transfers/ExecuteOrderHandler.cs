using Application.Abstractions.DataAccess;
using Application.Exceptions.NotFound;
using Domain.Transfers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.TransferOrders.ExecuteTransferOrder;

namespace Application.Transfers;

internal class ExecuteOrderHandler : IRequestHandler<Command>
{
    private readonly IDatabaseContext _context;

    public ExecuteOrderHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        var order = await _context.TransferOrders
            .Include(x => x.Operations)
            .ThenInclude(x => x.Student)
            .Include(x => x.Operations)
            .ThenInclude(x => x.Group)
            .SingleOrDefaultAsync(x => x.Id.Equals(request.OrderId), cancellationToken);

        if (order is null)
            throw EntityNotFoundException<TransferOrder>.Create(request.OrderId);

        order.Execute();

        _context.TransferOrders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}