using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class Mip_12 : OrionDevice
    {
        public const int Code = 48;
        public Mip_12(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "МИП-12";
            SupportedModels = new List<string>
            {
                Model
            };
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
