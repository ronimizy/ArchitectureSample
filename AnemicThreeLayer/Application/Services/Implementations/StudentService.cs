using Application.Dto;
using Application.Extensions;
using Application.Mapping;
using DataAccess;
using DataAccess.Models;

namespace Application.Services.Implementations;

internal class StudentService : IStudentService
{
    private readonly DatabaseContext _context;

    public StudentService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<StudentDto> CreateStudentAsync(string name, Guid groupId, CancellationToken cancellationToken)
    {
        var group = await _context.StudentGroups.GetEntityAsync(groupId, cancellationToken);
        var student = new Student(Guid.NewGuid(), name, group);

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        return student.AsDto();
    }
}