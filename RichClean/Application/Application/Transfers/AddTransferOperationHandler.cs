using Application.Abstractions.DataAccess;
using Application.Exceptions.NotFound;
using Application.Extensions;
using Application.Mapping;
using Domain.Transfers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Contracts.TransferOrders.AddTransferOperation;

namespace Application.Transfers;

internal class AddTransferOperationHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public AddTransferOperationHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var order = await _context.TransferOrders
            .Include(x => x.Operations)
            .SingleOrDefaultAsync(x => x.Id.Equals(request.OrderId), cancellationToken);

        if (order is null)
            throw EntityNotFoundException<TransferOrder>.Create(request.OrderId);

        var student = await _context.Students.GetEntityAsync(request.StudentId, cancellationToken);
        var group = await _context.StudentGroups.GetEntityAsync(request.GroupId, cancellationToken);

        var operation = new TransferOperation(Guid.NewGuid(), order, student, group);

        _context.TransferOperations.Add(operation);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(operation.AsDto());
    }
}