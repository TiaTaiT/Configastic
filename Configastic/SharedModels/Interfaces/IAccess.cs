using Configastic.SharedModels.Models.BolidDevices.ElectricModules;

namespace Configastic.SharedModels.Interfaces
{
    public interface IAccess
    {
        AccessController Access { get; set; }
    }
}
