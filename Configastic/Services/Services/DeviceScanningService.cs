using Configastic.Services.Interfaces;
using System.Net.NetworkInformation;

namespace Configastic.Services.Services
{
    public class DeviceScanningService : IDeviceScanningService
    {
        public async Task ScanNetworkAsync(
            string baseIp, 
            int startRange, 
            int endRange,
            Action<string> onDeviceFound, 
            Action<double> onProgressUpdate, 
            CancellationToken cancellationToken)
        {
            int total = endRange - startRange + 1;
            int current = 0;

            for (int i = startRange; i <= endRange; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                string ip = $"{baseIp}.{i}";
                bool isAlive = await PingHostAsync(ip);

                if (isAlive)
                {
                    onDeviceFound(ip);
                }

                current++;
                double progress = (double)current / total * 100;
                onProgressUpdate(progress);
            }
        }

        private static async Task<bool> PingHostAsync(string nameOrAddress)
        {
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(nameOrAddress, 1000);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
