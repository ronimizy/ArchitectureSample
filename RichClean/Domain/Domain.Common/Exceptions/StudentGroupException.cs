namespace Domain.Common.Exceptions;

public class StudentGroupException : DomainException
{
    private StudentGroupException(string? message) : base(message) { }

    public static StudentGroupException NewStudentLimitLessThenCurrentStudentCount(int count, int newValue)
        => new StudentGroupException($"New student limit {newValue} is less then current student count {count}");

    public static StudentGroupException ReachedStudentLimit(Guid groupId, int limit)
        => new StudentGroupException($"Reached student limit {limit} for group {groupId}");

    public static StudentGroupException StudentAlreadyExists(Guid groupId, Guid studentId)
        => new StudentGroupException($"Student {studentId} already exist in group {groupId}");

    public static StudentGroupException StudentDoesNotExist(Guid groupId, Guid studentId)
        => new StudentGroupException($"Student {studentId} does not exist in group {groupId}");
}