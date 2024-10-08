﻿namespace Configastic.SharedModels.Interfaces
{
    public interface IEthernetDevice : ICommunicationDevice
    {
        /// <summary>
        /// IP адрес прибора ("192.168.2.12")
        /// </summary>
        public string AddressIP { get; set; }

        public string Netmask { get; set; }

        /// <summary>
        /// MAC-адрес прибора
        /// </summary>
        public string MACaddress { get; set; }

        public string DefaultGateway { get; set; }

        public int CIDR
        {
            get;
            set;
        }

    }
}
