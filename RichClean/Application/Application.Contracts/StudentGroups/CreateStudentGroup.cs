using Application.Dto;
using MediatR;

namespace Application.Contracts.StudentGroups;

public static class CreateStudentGroup
{
    public record struct Command(string Name, int StudentLimit) : IRequest<Response>;

    public record struct Response(StudentGroupDto Group);
}