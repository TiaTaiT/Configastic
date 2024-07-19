namespace Configastic.Services.Interfaces
{
    public interface IDeviceScanningService
    {
        public Task ScanNetworkAsync(string baseIp, int startRange, int endRange,
            Action<string> onDeviceFound, Action<double> onProgressUpdate, CancellationToken cancellationToken);
    }
}
