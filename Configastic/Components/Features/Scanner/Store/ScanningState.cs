using Configastic.SharedModels.Interfaces;
using Fluxor;
using System.Collections.Immutable;

namespace Configastic.Components.Features.Scanner.Store
{
    public record ScanningState
    {
        public bool IsScanning { get; init; }
        public double Progress { get; init; }
        public ImmutableArray<IOrionDevice> FoundDevices { get; init; } = [];
        public bool ScannerMode {  get; init; }
    }

    public class ScanningFeature : Feature<ScanningState>
    {
        public override string GetName() => "Scanning";

        protected override ScanningState GetInitialState() => new()
        {
            IsScanning = false,
            Progress = 0,
            FoundDevices = [],
            ScannerMode = false,
        };
    }

    public record StartScanningAction();
    public record StopScanningAction();
    public record UpdateProgressAction(double Progress);
    public record AddFoundDeviceAction(IOrionDevice Device);
    public record UpdateFoundDeviceAction(IOrionDevice UpdatedDevice);
    public record ClearFoundDeviceAction();
    public record SetRenumberingModeAction();
    public record SetSearchingModeAction();
}
