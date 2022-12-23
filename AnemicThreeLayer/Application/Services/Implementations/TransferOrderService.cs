using Application.Dto;
using Application.Exceptions.NotFound;
using Application.Exceptions.Services;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

internal class TransferOrderService : ITransferOrderService
{
    private readonly DatabaseContext _context;

    public TransferOrderService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<TransferOrderDto> CreateTransferOrderAsync(CancellationToken cancellationToken)
    {
        var order = new TransferOrder(Guid.NewGuid());

        _context.TransferOrders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return order.AsDto();
    }

    public async Task<TransferOperationDto> AddTransferOperationAsync(
        Guid orderId,
        Guid studentId,
        Guid studentGroupId,
        CancellationToken cancellationToken)
    {
        TransferOrder order = await _context.TransferOrders.GetEntityAsync(orderId, cancellationToken);
        Student student = await _context.Students.GetEntityAsync(studentId, cancellationToken);
        StudentGroup studentGroup = await _context.StudentGroups.GetEntityAsync(studentGroupId, cancellationToken);

        var operation = new TransferOperation(Guid.NewGuid(), order, student, studentGroup);

        _context.TransferOperations.Add(operation);
        await _context.SaveChangesAsync(cancellationToken);

        return operation.AsDto();
    }

    public async Task ExecuteOrderAsync(Guid orderId, CancellationToken cancellationToken)
    {
        TransferOrder order = await _context.TransferOrders
            .Include(x => x.Operations)
            .ThenInclude(x => x.Group)
            .Include(x => x.Operations)
            .ThenInclude(x => x.Student)
            .SingleOrDefaultAsync(cancellationToken);

        if (order is null)
            throw EntityNotFoundException<TransferOrder>.Create(orderId);

        if (order.IsCompleted)
            throw TransferOrderException.OrderAlreadyCompleted(orderId);

        foreach (TransferOperation operation in order.Operations)
        {
            if (operation.Group.Students.Count.Equals(operation.Group.MaxStudentCount))
                throw TransferOrderException.GroupIsFull(operation.Group.Id);

            operation.Student.Group = operation.Group;
            _context.Students.Update(operation.Student);
        }

        order.IsCompleted = true;
        _context.TransferOrders.Update(order);

        await _context.SaveChangesAsync(cancellationToken);
    }
}