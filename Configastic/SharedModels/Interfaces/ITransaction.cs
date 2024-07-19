namespace Configastic.SharedModels.Interfaces
{
    public interface ITransaction
    {
        Task<byte[]> TransactionAsync(byte address, byte[] sendArray);
    }
}
