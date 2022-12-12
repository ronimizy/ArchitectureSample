using Application.Dto;
using Domain.Users;

namespace Application.Mapping;

public static class StudentGroupMapping
{
    public static StudentGroupDto AsDto(this StudentGroup group)
        => new StudentGroupDto(group.Id, group.Name, group.Students.Select(x => x.AsDto()).ToArray());
}