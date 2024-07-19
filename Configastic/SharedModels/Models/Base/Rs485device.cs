using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.Base
{
    public class Rs485device : CommunicationDevice, IRs485device
    {
        /// <summary>
        /// Адрес прибора на линии RS-485 ("23").
        /// </summary>
        public uint AddressRS485 { get; set; }     
    }
}
