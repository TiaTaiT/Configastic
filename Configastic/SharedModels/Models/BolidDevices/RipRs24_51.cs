
using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class RipRs24_51 : RipRs
    {
        public const int Code = 39;

        public RipRs24_51(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "РИП-24 исп.51";
            SupportedModels =
            [
                Model,
                "РИП-12 исп.50",
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
