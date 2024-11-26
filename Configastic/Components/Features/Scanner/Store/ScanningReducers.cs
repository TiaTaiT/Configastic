namespace Configastic.Components.Features.Scanner.Store
{
    using Fluxor;
    using System.Collections.Immutable;

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
            state with { FoundDevices = state.FoundDevices.Add(action.Device) };

        [ReducerMethod]
        public static ScanningState ReduceClearFoundDeviceAction(ScanningState state, ClearFoundDeviceAction action) =>
            state with { FoundDevices = state.FoundDevices.Clear() };

        [ReducerMethod]
        public static ScanningState ReduceUpdateFoundDeviceAction(ScanningState state, UpdateFoundDeviceAction action)
        {
            var updatedDevices = state.FoundDevices.Select(device =>
                device.Id == action.UpdatedDevice.Id ? action.UpdatedDevice : device).ToImmutableArray();

            return state with { FoundDevices = updatedDevices };
        }

        [ReducerMethod]
        public static ScanningState ReduceSetRenumberingModeAction(ScanningState state, SetRenumberingModeAction action) =>
            state with { ScannerMode = false };

        [ReducerMethod]
        public static ScanningState ReduceSetSearchingModeAction(ScanningState state, SetSearchingModeAction action) =>
            state with { ScannerMode = true };
    }
}
