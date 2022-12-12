using Application.Abstractions.DataAccess;
using Application.Mapping;
using Domain.Users;
using MediatR;
using static Application.Contracts.StudentGroups.CreateStudentGroup;

namespace Application.StudentGroups;

internal class CreateStudentGroupHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public CreateStudentGroupHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var group = new StudentGroup(Guid.NewGuid(), request.Name, new StudentCount(request.StudentLimit));

        _context.StudentGroups.Add(group);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(group.AsDto());
    }
}