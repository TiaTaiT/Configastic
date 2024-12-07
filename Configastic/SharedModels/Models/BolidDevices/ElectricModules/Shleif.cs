﻿using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices.ElectricModules
{
    public class Shleif: IShleif
    {
        private const int shleifValueIndex = 2;
        private const int getAdcValueCode = 0x1B;
        private const int getAdcState = 0x19;
        private readonly IOrionDevice _parentDevice;

        /// <summary>
        /// Input number Input 1, Input 2, ... Input 20 
        /// </summary>
        public byte ShleifIndex { get; set; } = 0;

        public InputTypes InputType { get; set; } = InputTypes.Intrusion;

        /// <summary>
        /// Zone
        /// </summary>
        public byte Zone { get; set; } = 0;
        
        /// <summary>
        /// Arming delay, n
        /// </summary>
        public byte ArmingDelay { get; set; } = 0;

        /// <summary>
        /// Input analysis delay after reset, s
        /// </summary>
        public byte InputAnalysisDelayAfterReset { get; set; } = 1;

        /// <summary>
        /// Shunt time, s
        /// </summary>
        public byte ShuntTime { get; set; } = 1;

        /// <summary>
        /// Activation delay 1
        /// </summary>
        public byte ActivationDelay1 { get; set; } = 0;

        /// <summary>
        /// Activation delay 2
        /// </summary>
        public byte ActivationDelay2 { get; set; } = 0;

        /// <summary>
        /// Activation delay 3
        /// </summary>
        public byte ActivationDelay3 { get; set; } = 0;

        /// <summary>
        /// Activation delay 4
        /// </summary>
        public byte ActivationDelay4 { get; set; } = 0;

        /// <summary>
        /// Relay activation delay 5
        /// </summary>
        public byte RelayActivationDelay5 { get; set; } = 0;

        /// <summary>
        /// Never disarm
        /// </summary>
        public bool NeverDisarm { get; set; } = false;

        /// <summary>
        /// Rearming if armed faild
        /// </summary>
        public bool RearmingIfArmingFailed { get; set; } = true;

        /// <summary>
        /// Rearming after alarm
        /// </summary>
        public bool RearmingAfterAlarm { get; set; } = false;

        /// <summary>
        /// Disarmed input monitoring
        /// </summary>
        public bool DisarmedInputMonitoring { get; set; } = false;

        /// <summary>
        /// Inhibit fire input monitoring
        /// </summary>
        public bool InhibitFireInputMonitoring { get; set; } = false;

        /// <summary>
        /// Debounce time 300 ms
        /// </summary>
        public bool DebounceTime { get; set; } = true;

        /// <summary>
        /// Ignore 10% of lobby input deviation
        /// </summary>
        public bool IgnoreLobbyInputDeviation { get; set; } = true;

        /// <summary>
        /// Output 1 (Alarm output 1)
        /// </summary>
        public bool Relay1 { get; set; } = false;

        /// <summary>
        /// Output 2 (Alarm output 2)
        /// </summary>
        public bool Relay2 { get; set; } = false;

        /// <summary>
        /// Relay 3 (Alarm output 3)
        /// </summary>
        public bool Relay3 { get; set; } = false;

        /// <summary>
        /// Relay 4 (Lamp)
        /// </summary>
        public bool Relay4 { get; set; } = false;

        /// <summary>
        /// Relay 5 (Siren)
        /// </summary>
        public bool Relay5 { get; set; } = false;

        public Shleif(IOrionDevice orionDevice, byte ShleifIndex)
        {
            _parentDevice = orionDevice;
            this.ShleifIndex = ++ShleifIndex;
        }

        /// <summary>
        /// Get current Analog-to-Digital converter value
        /// </summary>
        /// <returns>ADC current value</returns>
        public async Task<byte> GetShleifAdcValueAsync()
        {
            var address = (byte)_parentDevice.AddressRS485;
            var adcValue = await _parentDevice.AddressTransactionAsync(address, [getAdcValueCode, ShleifIndex, 0x00], IOrionNetTimeouts.Timeouts.addressChanging);
            return adcValue[shleifValueIndex];
        }

        public async Task<States> GetShleifStateAsync()
        {
            var address = (byte)_parentDevice.AddressRS485;
            var adcValue = await _parentDevice.AddressTransactionAsync(address, [getAdcState, ShleifIndex, 0x00], IOrionNetTimeouts.Timeouts.addressChanging);
            return adcValue[shleifValueIndex] switch
            {
                (byte)States.Alarm => States.Alarm,
                (byte)States.TakeFailed => States.TakeFailed,
                (byte)States.UnderGuard => States.UnderGuard,
                (byte)States.RemovedGuard => States.RemovedGuard,
                _ => States.Unknow
            };
        }
    }
}