using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configastic.SharedModels.Interfaces
{
    public interface IShleif
    {
        /// <summary>
        /// Input number Input 1, Input 2, ... Input 20 
        /// </summary>
        byte ShleifIndex { get; set; }

        InputTypes InputType { get; set; }

        /// <summary>
        /// Zone
        /// </summary>
        byte Zone { get; set; }

        /// <summary>
        /// Arming delay, n
        /// </summary>
        byte ArmingDelay { get; set; }

        /// <summary>
        /// Input analysis delay after reset, s
        /// </summary>
        byte InputAnalysisDelayAfterReset { get; set; }

        /// <summary>
        /// Shunt time, s
        /// </summary>
        byte ShuntTime { get; set; }

        /// <summary>
        /// Activation delay 1
        /// </summary>
        byte ActivationDelay1 { get; set; }

        /// <summary>
        /// Activation delay 2
        /// </summary>
        byte ActivationDelay2 { get; set; }

        /// <summary>
        /// Activation delay 3
        /// </summary>
        byte ActivationDelay3 { get; set; }

        /// <summary>
        /// Activation delay 4
        /// </summary>
        byte ActivationDelay4 { get; set; }

        /// <summary>
        /// Relay activation delay 5
        /// </summary>
        byte RelayActivationDelay5 { get; set; }

        /// <summary>
        /// Never disarm
        /// </summary>
        bool NeverDisarm { get; set; }

        /// <summary>
        /// Rearming if armed faild
        /// </summary>
        bool RearmingIfArmingFailed { get; set; }

        /// <summary>
        /// Rearming after alarm
        /// </summary>
        bool RearmingAfterAlarm { get; set; }

        /// <summary>
        /// Disarmed input monitoring
        /// </summary>
        bool DisarmedInputMonitoring { get; set; }

        /// <summary>
        /// Inhibit fire input monitoring
        /// </summary>
        bool InhibitFireInputMonitoring { get; set; }

        /// <summary>
        /// Debounce time 300 ms
        /// </summary>
        bool DebounceTime { get; set; }

        /// <summary>
        /// Ignore 10% of lobby input deviation
        /// </summary>
        bool IgnoreLobbyInputDeviation { get; set; }

        /// <summary>
        /// Output 1 (Alarm output 1)
        /// </summary>
        bool Relay1 { get; set; }

        /// <summary>
        /// Output 2 (Alarm output 2)
        /// </summary>
        bool Relay2 { get; set; }

        /// <summary>
        /// Relay 3 (Alarm output 3)
        /// </summary>
        bool Relay3 { get; set; }

        /// <summary>
        /// Relay 4 (Lamp)
        /// </summary>
        bool Relay4 { get; set; }

        /// <summary>
        /// Relay 5 (Siren)
        /// </summary>
        bool Relay5 { get; set; }

        /// <summary>
        /// Get current Analog-to-Digital converter value
        /// </summary>
        /// <returns>ADC current value</returns>
        Task<byte> GetShleifAdcValueAsync();

        Task<States> GetShleifStateAsync();
    }
}
