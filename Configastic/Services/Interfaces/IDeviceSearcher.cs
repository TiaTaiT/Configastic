using Configastic.SharedModels.Interfaces;

namespace Configastic.Services.Interfaces
{
    public interface IDeviceSearcher
    {
        Task SearchDevicesAsync(
            string ip,
            int localUdpPort,
            int remoteUdpPort,
            int startAddress,
            Action<IOrionDevice> onDeviceFound,
            Action<double> onProgressUpdate,
            CancellationToken cancellationToken);

        Task ChangeDefaultAddressToFirstFree(
            string ip,
            int localUdpPort,
            int remoteUdpPort,
            Action<IOrionDevice> onDeviceFound,
            Action cleanDeviceFound,
            CancellationToken cancellationToken);
    }
}