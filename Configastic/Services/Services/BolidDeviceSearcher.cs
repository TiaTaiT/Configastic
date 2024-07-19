using Configastic.Services.Interfaces;
using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices;
using Configastic.SharedModels.Models.Ports;

namespace Configastic.Services.Services
{
    public class BolidDeviceSearcher : IDeviceSearcher
    {
        public async Task SearchDevicesAsync(
            int localUdpPort,
            int remoteUdpPort,
            Action<IOrionDevice> onDeviceFound,
            Action<double> onProgressUpdate,
            CancellationToken cancellationToken)
        {
            var port = new BolidUdpClient(40000)
            {
                ClientUdpPort = localUdpPort,
                RemoteServerUdpPort = remoteUdpPort,
            };
            var c2000M = new C2000M(port);
            var t = await c2000M.SearchOnlineDevices(onDeviceFound, onProgressUpdate, cancellationToken);
        }
    }
}
