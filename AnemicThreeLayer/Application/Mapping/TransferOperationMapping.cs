using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class TransferOperationMapping
{
    public static TransferOperationDto AsDto(this TransferOperation operation)
        => new TransferOperationDto(operation.Id, operation.Order.Id, operation.Student.Id, operation.Group.Id);
}