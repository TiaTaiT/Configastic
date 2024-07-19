using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_k : OrionDevice
    {
        public const int Code = 7;
        public C2000_k(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-К";
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
