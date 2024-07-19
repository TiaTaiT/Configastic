namespace Configastic.Components.Features.Scanner.Store
{
    using Fluxor;

    public static class ScanningReducers
    {
        [ReducerMethod]
        public static ScanningState ReduceStartScanningAction(ScanningState state, StartScanningAction action) =>
            state with { IsScanning = true, Progress = 0, FoundDevices = [] };

        [ReducerMethod]
        public static ScanningState ReduceStopScanningAction(ScanningState state, StopScanningAction action) =>
            state with { IsScanning = false };

        [ReducerMethod]
        public static ScanningState ReduceUpdateProgressAction(ScanningState state, UpdateProgressAction action) =>
            state with { Progress = action.Progress };

        [ReducerMethod]
        public static ScanningState ReduceAddFoundDeviceAction(ScanningState state, AddFoundDeviceAction action) =>
            state with { FoundDevices = new List<string>(state.FoundDevices) { action.Device } };
    }
}
