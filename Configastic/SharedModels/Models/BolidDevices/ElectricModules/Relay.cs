﻿using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices.ElectricModules
{
    public class Relay(IOrionDevice orionDevice, byte relayIndex)
    {
        protected IOrionDevice parentDevice = orionDevice;

        public enum OutputTypes : byte
        {
            Standard = 1,
            Auxilary = 2,
            FireEquiped = 3
        }

        public enum ControlPrograms : byte
        {
            NoControl = 0,
            TurnOn = 1,
            TurnOff = 2,
            TurnOnForTime = 3,
            TurnOffForTime = 4,
            Blink_OffInitialPosition = 5,
            Blink_OnInitialPosition = 6,
            BlinkForTime_OffInitialPosition = 7,
            BlinkForTime_OnInitialPosition = 8,
            Lamp = 9,
            AlarmOutput1 = 10,
            ASPT = 11,
            Siren = 12,
            FireOutput = 13,
            FaultOutput = 14,
            FireLamp = 15,
            AlarmOutput2 = 16,
            TurnOnForTimeBeforeArming = 17,
            TurnOffForTimeBeforeArming = 18,
            TurnOnForTimeUponArming = 19,
            TurnOffForTimeUponArming = 20,
            TurnOnForTimeUponDisarming = 21,
            TurnOffForTimeUponDisarming = 22,
            TurnOnForTimeIfArmingFailed = 23,
            TurnOffForTimeIfArmingFailed = 24,
            TurnOnForTimeUponAuxActivation = 25,
            TurnOffForTimeUponAuxActivation = 26,
            TurnOnUponDisarming = 27,
            TurnOffUponDisarming = 28,
            TurnOnUponArming = 29,
            TurnOffUponArming = 30,
            TurnOnUponAuxActivation = 31,
            TurnOffUponAuxActivation = 32,
            ASPT_1 = 33,
            ASPT_A = 34,
            ASPT_A1 = 35,
            TurnOnIfIncreased = 36,
            TurnOnIfDecreased = 37,
            TurnOnIfFire2 = 50,
            TurnOffIfFire2 = 51,
            BlinkForTimeIfFire2_OffInitialPosition = 52,
            BlinkForTimeIfFire2_OnInitialPosition = 53,
            TurnOnIfAttack = 54,
            TurnOffIfAttack = 55
        }

        /// <summary>
        /// Relay number Relay 1, Relay, 2, Relay 3 etc.
        /// </summary>
        public byte RelayIndex { get; set; } = ++relayIndex; // внутренняя нумерация в приборах Болид начинается с единицы
        public OutputTypes OutputType { get; set; } = OutputTypes.Standard;
        public ControlPrograms ControlProgram { get; set; } = ControlPrograms.NoControl;
        public ushort ControlTime { get; set; } = 0xFFFF;
        public bool Events { get; set; } = false;
        public bool EndlessControlTime = true;

        public async Task<bool> TurnOnAsync()
        {
            var serialPort = parentDevice.Port;
            var address = (byte)parentDevice.AddressRS485;
            var result = await parentDevice.AddressTransactionAsync(address, [0x15, RelayIndex, 0x01], IOrionNetTimeouts.Timeouts.addressChanging);
            
            if (result == null || result.Length < 3)
                return false;

            if (result[1] == RelayIndex && result[2] == 0x01)
                return true;

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
                return true;

            return false;
        }
    }
}
