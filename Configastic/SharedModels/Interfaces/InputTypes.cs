namespace Configastic.SharedModels.Interfaces
{
    public enum InputTypes : byte
    {
        SmokeFire = 0,
        CombinedFire = 1,
        HeatFire = 2,
        Intrusion = 3,
        IntrusionAndTamper = 4,
        Auxilary = 5,
        Lobby = 6,
        Panic = 10,
        AuxProgrammable = 11,
        FireManualCallPoint = 15,
        FloodAlarm = 16,
        ManualRelease = 17
    }
}
