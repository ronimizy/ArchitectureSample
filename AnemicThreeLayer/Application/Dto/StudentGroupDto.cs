namespace Application.Dto;

public record StudentGroupDto(Guid Id, string Name, IReadOnlyCollection<StudentDto> Students);