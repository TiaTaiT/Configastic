using Configastic.Services.Interfaces;
using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices;
using Configastic.SharedModels.Models.Ports;
using System.Net;

namespace Configastic.Services.Services
{
    public class BolidDeviceSearcher(Fluxor.IDispatcher dispatcher) : IDeviceSearcher
    {
        private readonly Fluxor.IDispatcher _dispatcher = dispatcher;

        public async Task ChangeDefaultAddressToFirstFree(
            string ip,
            int localUdpPort,
            int remoteUdpPort,
            Action<IOrionDevice> onDeviceFound,
            Action cleanDeviceFound,
            CancellationToken cancellationToken)
        {
            var c2000M = GetOrionManager(ip, localUdpPort, remoteUdpPort);
            c2000M.Port.MaxRepetitions = 1;
            var startAddress = 1;
            while (!cancellationToken.IsCancellationRequested)
            {
                IOrionDevice newDevice;
                try
                {
                    newDevice = await c2000M.GetNewOnlineDevice(127);

                }
                catch (Exception ex)
                {
                    continue;
                }
                if (newDevice == null)
                    continue;

                var freeAddress = await c2000M.GetFirstFreeAddress(startAddress, onDeviceFound, cleanDeviceFound, cancellationToken);
                if (freeAddress > 0)
                {
                    newDevice.AddressRS485 = freeAddress;
                    await newDevice.SetAddressAsync();
                    //_dispatcher.Dispatch(new UpdateFoundDeviceAction(newDevice));
                    onDeviceFound(newDevice);
                }
            }

        }

        public async Task SearchDevicesAsync(
            string ip,
            int localUdpPort,
            int remoteUdpPort,
            int startAddress,
            Action<IOrionDevice> onDeviceFound,
            Action<double> onProgressUpdate,
            CancellationToken cancellationToken)
        {
            var c2000M = GetOrionManager(ip, localUdpPort, remoteUdpPort);
            var t = await c2000M.SearchOnlineDevices(startAddress, onDeviceFound, onProgressUpdate, cancellationToken);
        }

        private static C2000M GetOrionManager(string ip, int localUdpPort, int remoteUdpPort)
        {
            var port = new BolidUdpClient(IPAddress.Parse(ip), remoteUdpPort, localUdpPort)
            {
                RemoteServerUdpPort = remoteUdpPort,
            };
            return new C2000M(port);
        }
    }
}
