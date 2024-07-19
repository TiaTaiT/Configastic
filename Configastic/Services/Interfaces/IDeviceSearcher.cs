using Configastic.SharedModels.Interfaces;

namespace Configastic.Services.Interfaces
{
    public interface IDeviceSearcher
    {
        Task SearchDevicesAsync(
            int localUdpPort,
            int remoteUdpPort,
            Action<IOrionDevice> onDeviceFound,
            Action<double> onProgressUpdate,
            CancellationToken cancellationToken);
    }
}
