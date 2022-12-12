using Domain.Common.Exceptions;

namespace Domain.Transfers;

public class TransferOrder
{
    private readonly HashSet<TransferOperation> _operations;

    public TransferOrder(Guid id)
    {
        Id = id;
        _operations = new HashSet<TransferOperation>();
    }

#pragma warning disable CS8618
    protected TransferOrder() { }
#pragma warning restore CS8618

    public Guid Id { get; protected init; }

    public bool IsCompleted { get; protected set; }
    public virtual IReadOnlyCollection<TransferOperation> Operations => _operations;

    public void AddOperation(TransferOperation operation)
    {
        if (IsCompleted)
            throw TransferOrderException.AlreadyCompleted(Id);

        if (_operations.Add(operation) is false)
            throw TransferOrderException.OperationAlreadyExists(Id, operation.Id);
    }

    public void RemoveOperation(TransferOperation operation)
    {
        if (IsCompleted)
            throw TransferOrderException.AlreadyCompleted(Id);

        if (_operations.Remove(operation) is false)
            throw TransferOrderException.OperationNotFound(Id, operation.Id);
    }

    public void Execute()
    {
        if (IsCompleted)
            throw TransferOrderException.AlreadyCompleted(Id);

        foreach (var operation in _operations)
        {
            operation.Execute();
        }

        IsCompleted = true;
    }
}