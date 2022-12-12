using Application.Dto;

namespace Application.Services;

public interface IStudentGroupService
{
    Task<StudentGroupDto> CreateStudentGroupAsync(string name, int maxStudentCount, CancellationToken cancellationToken);
}