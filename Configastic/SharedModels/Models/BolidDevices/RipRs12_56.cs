using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class RipRs12_56 : RipRs
    {
        public const int Code = 54;

        public RipRs12_56(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "РИП-12 исп.56";
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
