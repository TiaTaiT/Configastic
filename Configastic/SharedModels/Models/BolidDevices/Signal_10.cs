using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.BolidDevices.ElectricModules;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class Signal_10 : OrionDevice
    {
        private readonly int inputsCount = 10;
        private readonly int relayNumber = 2;
        private const int sirenTime = 0x03C0;

        public const int Code = 34;

        #region Properties
        public IEnumerable<Shleif> Shleifs { get; set; }
        public IEnumerable<Relay> Relays { get; set; }
        public IEnumerable<SupervisedRelay> SupervisedRelays { get; }
        #endregion Properties

        public Signal_10(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "Сигнал-10";
            SupportedModels =
            [
                Model,
            ];

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

        public override bool Setup(Action<int> updateProgressBar, int modelCode = 0)
        {
            return base.Setup(updateProgressBar, Code);
        }
    }
}
