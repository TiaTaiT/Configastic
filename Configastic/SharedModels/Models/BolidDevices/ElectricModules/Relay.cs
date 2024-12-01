using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices.ElectricModules
{
    public class Relay(IOrionDevice orionDevice, byte relayIndex): IRelay
    {
        private bool _currentRelayState;

        protected IOrionDevice parentDevice = orionDevice;

        /// <summary>
        /// Relay number Relay 1, Relay, 2, Relay 3 etc.
        /// </summary>
        public byte RelayIndex { get; set; } = ++relayIndex; // внутренняя нумерация в приборах Болид начинается с единицы
        public OutputTypes OutputType { get; set; } = OutputTypes.Standard;
        public ControlPrograms ControlProgram { get; set; } = ControlPrograms.NoControl;
        public ushort ControlTime { get; set; } = 0xFFFF;
        public bool Events { get; set; } = false;
        
        bool IRelay.EndlessControlTime { get; set; } = true;

        public async Task<bool> TurnOnAsync()
        {
            var serialPort = parentDevice.Port;
            var address = (byte)parentDevice.AddressRS485;
            var result = await parentDevice.AddressTransactionAsync(address, [0x15, RelayIndex, 0x01], IOrionNetTimeouts.Timeouts.addressChanging);
            
            if (result == null || result.Length < 3)
                return false;

            if (result[1] == RelayIndex && result[2] == 0x01)
            {
                _currentRelayState = true;
                return true;
            }
            return false;
        }
        public async Task<bool> TurnOffAsync()
        {
            var serialPort = parentDevice.Port;
            var address = (byte)parentDevice.AddressRS485;
            var result = await parentDevice.AddressTransactionAsync(address, [0x15, RelayIndex, 0x02], IOrionNetTimeouts.Timeouts.addressChanging);
            
            if (result == null || result.Length < 3)
                return false;

            if (result[1] == RelayIndex && result[2] == 0x02)
            {
                _currentRelayState = false;
                return true;
            }
            return false;
        }

        public Task<bool> GetCurrentRelayState()
        {
            return Task.FromResult(_currentRelayState);
        }
    }
}
