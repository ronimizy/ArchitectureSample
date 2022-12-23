namespace Application.Exceptions.Services;

public class TransferOrderException : ApplicationException
{
    private TransferOrderException(string message)
        : base(message) { }

    public static TransferOrderException OrderAlreadyCompleted(Guid orderId)
    {
        return new TransferOrderException($"Order {orderId} is already completed.");
    }

    public static TransferOrderException GroupIsFull(Guid groupId)
    {
        return new TransferOrderException($"Group {groupId} is full.");
    }
}