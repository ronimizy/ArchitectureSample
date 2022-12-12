using Application.Dto;
using MediatR;

namespace Application.Contracts.Students;

public static class CreateStudent
{
    public record struct Command(string Name, Guid GroupId) : IRequest<Response>;

    public record struct Response(StudentDto Student);
}