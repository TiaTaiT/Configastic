using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_pge : OrionDevice
    {
        public const int Code = 43;
        public C2000_pge(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-PGE";
            SupportedModels =
            [
                Model,
                "С2000-PGE исп.01"
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
