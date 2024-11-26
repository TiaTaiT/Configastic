using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices.ElectricModules;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000_4 : OrionDevice, IRelays, IShleifs
    {
        private readonly int inputsCount = 4;
        private readonly int relayNumber = 2;
        private const int sirenTime = 0x03C0;

        public const int Code = 4;

        #region Properties
        public IEnumerable<Shleif> Shleifs { get; set; }
        public IEnumerable<Relay> Relays { get; set; }
        public IEnumerable<SupervisedRelay> SupervisedRelays { get; }
        #endregion Properties

        public C2000_4(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000-4";
            SupportedModels = new List<string>
            {
                Model,
            };

            var inputs = new List<Shleif>();
            for (byte i = 0; i < inputsCount; i++)
            {
                inputs.Add(new Shleif(this, i));
            }
            Shleifs = inputs;

            var relays = new List<Relay>();

            for (byte i = 0; i < relayNumber; i++)
            {
                relays.Add(new Relay(this, i));
            }

            Relays = relays;

            var supervisedRelays = new List<SupervisedRelay>
            {
                new(this, 2),
                new(this, 3)
                {
                    ControlTime = sirenTime
                }
            };


            SupervisedRelays = supervisedRelays;
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }
    }
}
