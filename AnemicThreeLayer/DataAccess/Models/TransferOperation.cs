namespace DataAccess.Models;

public class TransferOperation
{
    public TransferOperation(Guid id, TransferOrder order, Student student, StudentGroup group)
    {
        Id = id;
        Order = order;
        Student = student;
        Group = group;
    }

    protected TransferOperation() { }

    public Guid Id { get; set; }

    public virtual TransferOrder Order { get; set; }
    public virtual Student Student { get; set; }
    public virtual StudentGroup Group { get; set; }
}