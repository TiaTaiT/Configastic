using Configastic.SharedModels.Models.Utils;

namespace Configastic.Services.Interfaces
{
    public interface IGoogleDriveSheetsLister
    {
        Task<IEnumerable<ProjectHeader>> ListAllSpreadsheetsAsync();
    }
}
