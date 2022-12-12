using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Users;
using MediatR;
using static Application.Contracts.Students.CreateStudent;

namespace Application.Students;

internal class CreateStudentHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateStudentHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var group = await _context.StudentGroups.GetEntityAsync(request.GroupId, cancellationToken);
        var student = new Student(Guid.NewGuid(), request.Name, group);

        _context.Students.Add(student);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(student.AsDto());
    }
}