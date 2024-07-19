using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_aspt : OrionDevice
    {
        public const int Code = 14;

        public C2000_aspt(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-АСПТ";
            SupportedModels =
            [
                Model,
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
