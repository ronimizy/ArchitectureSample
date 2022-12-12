namespace DataAccess.Models;

public class Student
{
    public Student(Guid id, string name, StudentGroup group)
    {
        Id = id;
        Name = name;
        Group = group;
    }

    protected Student() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual StudentGroup Group { get; set; }
}