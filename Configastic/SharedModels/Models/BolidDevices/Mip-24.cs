using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class Mip_24 : OrionDevice
    {
        public const int Code = 49;
        public Mip_24(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "МИП-24";
            SupportedModels =
            [
                Model
            ];
        }

        public override bool Setup(Action<int> updateProgressBar, int modelCode = 0)
        {
            return base.Setup(updateProgressBar, Code);
        }
    }
}
