namespace DataAccess.Models;

public class StudentGroup
{
    public StudentGroup(Guid id, string name, int maxStudentCount)
    {
        Id = id;
        Name = name;
        MaxStudentCount = maxStudentCount;
        Students = new List<Student>();
    }

    protected StudentGroup() { }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int MaxStudentCount { get; set; }

    public virtual ICollection<Student> Students { get; set; }
}