using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices.ElectricModules;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class Signal20 : OrionDevice, IRelays, IShleifs
    {
        public const int Code = 1;
        public Signal20(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "Сигнал-20";
            SupportedModels =
            [
                Model
            ];
        }

        public IEnumerable<Relay> Relays { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<Shleif> Shleifs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
