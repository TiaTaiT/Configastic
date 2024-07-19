using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class RipRs_48 : RipRs
    {
        public const int Code = 53;

        public RipRs_48(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "РИП-48 исп.01";
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
