namespace Application.Dto;

public record TransferOperationDto(Guid Id, Guid OrderId, Guid StudentId, Guid GroupId);