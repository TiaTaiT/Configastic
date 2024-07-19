using Configastic.Services.Interfaces;
using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices;

namespace Configastic.Services.Services
{
    public class BolidDeviceSearcher : IDeviceSearcher
    {
        public required IPort Port { get; set; }

        public IEnumerable<IOrionDevice> SearchDevices(Action<int> updateProgressBar, CancellationToken cancellationToken)
        {
            var c2000M = new C2000M(Port);
            var onlineDevices = c2000M.SearchOnlineDevices(updateProgressBar, cancellationToken);

            return onlineDevices;
        }
    }
}
