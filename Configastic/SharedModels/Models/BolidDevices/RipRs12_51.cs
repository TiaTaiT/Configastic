using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class RipRs12_51 : RipRs
    {
        public const int Code = 33;

        public RipRs12_51(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "РИП-12 исп.51";
            SupportedModels =
            [
                Model,
                "РИП-12 исп.50",
                "РИП-12",
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
