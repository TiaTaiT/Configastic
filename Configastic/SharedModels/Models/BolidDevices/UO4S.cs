using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class UO4S : OrionDevice
    {
        public const int Code = 24;

        public UO4S(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "УО-4С";
            SupportedModels =
            [
                Model,
                "УО-4С исп.02"
            ];
        }

        public override bool Setup(Action<int> updateProgressBar, int modelCode = 0)
        {
            return base.Setup(updateProgressBar, Code);
        }
    }
}
