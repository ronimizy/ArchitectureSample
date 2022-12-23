using Application.Dto;
using DataAccess.Models;

namespace Application.Mapping;

public static class StudentMapping
{
    public static StudentDto AsDto(this Student student)
    {
        return new StudentDto(student.Id, student.Name, student.Group.Id);
    }
}