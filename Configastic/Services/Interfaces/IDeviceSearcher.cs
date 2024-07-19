using Configastic.SharedModels.Interfaces;

namespace Configastic.Services.Interfaces
{
    internal interface IDeviceSearcher
    {
        IPort Port { get; set; }

        IEnumerable<IOrionDevice> SearchDevices(Action<int> updateProgressBar, CancellationToken cancellationToken);
    }
}
