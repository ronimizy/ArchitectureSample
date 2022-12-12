using Application.Dto;
using Domain.Users;

namespace Application.Mapping;

public static class StudentMapping
{
    public static StudentDto AsDto(this Student student)
        => new StudentDto(student.Id, student.Name, student.Group.Id);
}