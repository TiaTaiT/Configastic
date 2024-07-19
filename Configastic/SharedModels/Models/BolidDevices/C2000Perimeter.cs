using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000Perimeter : OrionDevice
    {
        public const int Code = 47;

        public C2000Perimeter(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-Периметр";
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
