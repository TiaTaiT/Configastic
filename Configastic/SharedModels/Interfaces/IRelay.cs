namespace Configastic.SharedModels.Interfaces
{
    public interface IRelay
    {
        byte RelayIndex { get; set; }
        ushort ControlTime { get; set; }
        bool Events { get; set; }
        bool EndlessControlTime { get; set; }

        Task<bool> TurnOnAsync();
        Task<bool> TurnOffAsync();
        Task<bool> GetCurrentRelayState();
    }
}
