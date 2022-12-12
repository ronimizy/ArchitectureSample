using Domain.Common.Exceptions;

namespace Domain.Users;

public class StudentGroup : IEquatable<StudentGroup>
{
    private readonly HashSet<Student> _students;
    private StudentCount _studentLimit;

    public StudentGroup(Guid id, string name, StudentCount studentLimit)
    {
        Id = id;
        Name = name;
        _studentLimit = studentLimit;
        _students = new HashSet<Student>();
    }

#pragma warning disable CS8618
    protected StudentGroup() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }
    public string Name { get; set; }

    public StudentCount StudentLimit
    {
        get => _studentLimit;
        set => _studentLimit = EnsureCanUpdateMaxStudentCount(value);
    }

    public virtual IReadOnlyCollection<Student> Students => _students;

    public void AddStudent(Student student)
    {
        if (_students.Count.Equals(StudentLimit.Value))
            throw StudentGroupException.ReachedStudentLimit(Id, StudentLimit.Value);

        if (_students.Add(student) is false)
            throw StudentGroupException.StudentAlreadyExists(Id, student.Id);
    }

    public void RemoveStudent(Student student)
    {
        if (_students.Remove(student) is false)
            throw StudentGroupException.StudentDoesNotExist(Id, student.Id);
    }

    public bool Equals(StudentGroup? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as StudentGroup);

    public override int GetHashCode()
        => Id.GetHashCode();

    private StudentCount EnsureCanUpdateMaxStudentCount(StudentCount value)
    {
        if (value.Value < _students.Count)
            throw StudentGroupException.NewStudentLimitLessThenCurrentStudentCount(_students.Count, value.Value);

        return value;
    }
}