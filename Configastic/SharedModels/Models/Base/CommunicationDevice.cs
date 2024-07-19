using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.Ports;

namespace Configastic.SharedModels.Models.Base
{
    public abstract class CommunicationDevice : Device, ICommunicationDevice
    {
        public IPort Port { get; set; }
    }
}
