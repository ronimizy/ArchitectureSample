using Domain.Common.Exceptions;

namespace Domain.Users;

public class Student : IEquatable<Student>
{
    public Student(Guid id, string name, StudentGroup group)
    {
        Id = id;
        Name = name;
        Group = group;
    }

#pragma warning disable CS8618
    public Student() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }
    public string Name { get; set; }

    public virtual StudentGroup Group { get; protected set; }

    public void TransferTo(StudentGroup group)
    {
        if (Group.Equals(group))
            throw StudentException.TransferToSameGroup(Id);
        
        group.AddStudent(this);
        Group.RemoveStudent(this);

        Group = group;
    }

    public bool Equals(Student? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as Student);

    public override int GetHashCode()
        => Id.GetHashCode();

    public override string ToString()
        => $"[{Id.ToString()}] {Name}";
}