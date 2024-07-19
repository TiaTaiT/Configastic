namespace Configastic.SharedModels.Interfaces
{
    public interface ITransaction
    {
        public byte[] Transaction(byte address, byte[] sendArray);
    }
}
