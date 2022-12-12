using Domain.Users;

namespace Domain.Transfers;

public class TransferOperation : IEquatable<TransferOperation>
{
    public TransferOperation(Guid id, TransferOrder order, Student student, StudentGroup group)
    {
        Id = id;
        Order = order;
        Student = student;
        Group = group;

        order.AddOperation(this);
    }

#pragma warning disable CS8618
    protected TransferOperation() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }

    public virtual TransferOrder Order { get; protected init; }
    public virtual Student Student { get; protected init; }
    public virtual StudentGroup Group { get; protected init; }

    public void Execute()
        => Student.TransferTo(Group);

    public bool Equals(TransferOperation? other)
        => other?.Id.Equals(Id) ?? false;

    public override bool Equals(object? obj)
        => Equals(obj as TransferOperation);

    public override int GetHashCode()
        => Id.GetHashCode();
}