using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class TransferOrderMapping
{
    public static TransferOrderDto AsDto(this TransferOrder transferOrder)
        => new TransferOrderDto(transferOrder.Id);
}