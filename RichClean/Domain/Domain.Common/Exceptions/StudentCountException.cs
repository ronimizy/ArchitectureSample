namespace Domain.Common.Exceptions;

public class StudentCountException : DomainException
{
    private StudentCountException(string? message) : base(message) { }

    public static StudentCountException InvalidValue(int value)
        => new StudentCountException($"Invalid value: {value}");
}