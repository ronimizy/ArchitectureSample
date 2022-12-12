namespace Domain.Common.Exceptions;

public class TransferOrderException : DomainException
{
    private TransferOrderException(string? message) : base(message) { }

    public static TransferOrderException AlreadyCompleted(Guid id)
        => new TransferOrderException($"Transfer order with id {id} is already completed");

    public static TransferOrderException OperationAlreadyExists(Guid orderId, Guid operationId)
    {
        var message = $"Operation with id {operationId} already exist in transfer order with id {orderId}";
        return new TransferOrderException(message);
    }
    
    public static TransferOrderException OperationNotFound(Guid orderId, Guid operationId)
    {
        var message = $"Operation with id {operationId} not found in transfer order with id {orderId}";
        return new TransferOrderException(message);
    }
}