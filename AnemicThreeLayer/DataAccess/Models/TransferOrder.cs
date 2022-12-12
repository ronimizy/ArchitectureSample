namespace DataAccess.Models;

public class TransferOrder
{
    public TransferOrder(Guid id)
    {
        Id = id;
    }

    protected TransferOrder() { }

    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
    public virtual ICollection<TransferOperation> Operations { get; set; }
}