using Configastic.SharedModels.Models.BolidDevices;
using Configastic.SharedModels.Models.Utils;

namespace Configastic.SharedModels.Interfaces
{
    public class EthernetOrionDevice : OrionDevice, IEthernetDevice
    {
        public EthernetOrionDevice(IPort port) : base(port)
        {
        }

        /// <summary>
        /// IP адрес прибора ("192.168.2.12")
        /// </summary>
        public string AddressIP { get; set; }

        public string Netmask { get; set; }

        public int CIDR
        {
            get => EthernetUtils.ConvertToCidr(Netmask);
            set => Netmask = EthernetUtils.CidrToString(value);
        }

        /// <summary>
        /// MAC-адрес прибора
        /// </summary>
        public string MACaddress { get; set; }

        public string DefaultGateway { get; set; }
    }
}
