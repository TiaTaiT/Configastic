using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices.ElectricModules;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000sp1 : OrionDevice
    {
        private readonly int relayNumber = 4;

        public const int Code = 3;
        public IEnumerable<Relay> Relays { get; set; }

        public C2000sp1(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-СП1";
            SupportedModels =
            [
                Model,
            ];

            var relays = new List<Relay>();

            for (byte i = 0; i < relayNumber; i++)
            {
                relays.Add(new Relay(this, i));
            }

            Relays = relays;
        }
        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
