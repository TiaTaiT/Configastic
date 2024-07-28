using Configastic.Services.Interfaces;
using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices;
using Configastic.SharedModels.Models.Ports;

namespace Configastic.Services.Services
{
    public class BolidDeviceSearcher(Fluxor.IDispatcher dispatcher) : IDeviceSearcher
    {
        private readonly Fluxor.IDispatcher _dispatcher = dispatcher;

        public async Task ChangeDefaultAddressToFirstFree(
            int localUdpPort, 
            int remoteUdpPort,
            Action<IOrionDevice> onDeviceFound,
            Action cleanDeviceFound,
            CancellationToken cancellationToken)
        {
            var c2000M = GetOrionManager(localUdpPort, remoteUdpPort);
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
            int localUdpPort,
            int remoteUdpPort,
            int startAddress,
            Action<IOrionDevice> onDeviceFound,
            Action<double> onProgressUpdate,
            CancellationToken cancellationToken)
        {
            var c2000M = GetOrionManager(localUdpPort, remoteUdpPort);
            var t = await c2000M.SearchOnlineDevices(startAddress, onDeviceFound, onProgressUpdate, cancellationToken);
        }

        private static C2000M GetOrionManager(int localUdpPort, int remoteUdpPort)
        {
            var port = new BolidUdpClient(localUdpPort)
            {
                RemoteServerUdpPort = remoteUdpPort,
            };
            return new C2000M(port);
        }
    }
}
