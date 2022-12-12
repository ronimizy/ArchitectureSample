using Application.Dto;

namespace Application.Services;

public interface IStudentService
{
    Task<StudentDto> CreateStudentAsync(string name, Guid groupId, CancellationToken cancellationToken);
}