namespace Configastic.SharedModels.Interfaces
{
    public interface IRs485device : ICommunicationDevice
    {
        /// <summary>
        /// Адрес прибора на линии RS-485 ("23").
        /// </summary>
        public uint AddressRS485 { get; set; }
    }
}
