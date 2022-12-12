namespace Domain.Common.Exceptions;

public class StudentException : DomainException
{
    private StudentException(string? message) : base(message) { }

    public static StudentException TransferToSameGroup(Guid studentId)
        => new StudentException($"Student {studentId} is already in this group");
}