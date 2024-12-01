using Configastic.SharedModels.Models.BolidDevices.ElectricModules;

namespace Configastic.SharedModels.Interfaces
{
    internal interface IRelays
    {
        IEnumerable<Relay> Relays { get; set; }
    }
}
