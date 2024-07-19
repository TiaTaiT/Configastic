using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.Base
{
    public abstract class CommunicationDevice : Device, ICommunicationDevice
    {
        public IPort? Port { get; set; }
    }
}
