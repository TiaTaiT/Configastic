using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public abstract class RipRs(IPort port) : OrionDevice(port)
    {
    }
}
