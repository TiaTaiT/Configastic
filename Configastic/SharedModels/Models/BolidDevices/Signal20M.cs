using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class Signal20M : Signal20P
    {
        public new const int Code = 26;

        public Signal20M(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "Сигнал-20М";
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
