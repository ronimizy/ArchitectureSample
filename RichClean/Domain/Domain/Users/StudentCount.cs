using Domain.Common.Exceptions;

namespace Domain.Users;

public readonly record struct StudentCount
{
    public StudentCount(int value)
    {
        if (value < 0)
            throw StudentCountException.InvalidValue(value);

        Value = value;
    }

    public int Value { get; }

    public override string ToString()
        => Value.ToString();
}