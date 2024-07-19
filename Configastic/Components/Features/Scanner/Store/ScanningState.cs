using Fluxor;

namespace Configastic.Components.Features.Scanner.Store
{
    public record ScanningState
    {
        public bool IsScanning { get; init; }
        public double Progress { get; init; }
        public List<string> FoundDevices { get; init; } = [];
    }

    public class ScanningFeature : Feature<ScanningState>
    {
        public override string GetName() => "Scanning";

        protected override ScanningState GetInitialState() => new()
        {
            IsScanning = false,
            Progress = 0,
            FoundDevices = [],
        };
    }

    public record StartScanningAction();
    public record StopScanningAction();
    public record UpdateProgressAction(double Progress);
    public record AddFoundDeviceAction(string Device);
}
