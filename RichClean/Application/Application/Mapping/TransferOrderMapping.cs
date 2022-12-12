using Application.Dto;
using Domain.Transfers;

namespace Application.Mapping;

public static class TransferOrderMapping
{
    public static TransferOrderDto AsDto(this TransferOrder transferOrder)
        => new TransferOrderDto(transferOrder.Id);
}