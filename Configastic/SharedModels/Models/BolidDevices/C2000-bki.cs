using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_bki : OrionDevice
    {
        public const int Code = 45;
        public C2000_bki(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "Поток-БКИ";
            SupportedModels =
            [
                Model
            ];
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
