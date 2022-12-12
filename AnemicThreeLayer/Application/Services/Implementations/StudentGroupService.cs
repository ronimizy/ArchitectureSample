using Application.Dto;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

internal class StudentGroupService : IStudentGroupService
{
    private readonly DatabaseContext _context;

    public StudentGroupService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<StudentGroupDto> CreateStudentGroupAsync(string name, int maxStudentCount, CancellationToken cancellationToken)
    {
        var group = new StudentGroup(Guid.NewGuid(), name, maxStudentCount);

        _context.StudentGroups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);

        return group.AsDto();
    }
}