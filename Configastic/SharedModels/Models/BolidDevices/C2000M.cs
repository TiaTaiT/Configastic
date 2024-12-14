using Configastic.Exceptions;
using Configastic.SharedModels.Interfaces;
using System.Diagnostics;
using System.Net;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class C2000M : OrionDevice
    {
        public const int Code = 0;

        private readonly Dictionary<byte, Func<IOrionDevice>> _bolidDict;

        private int _id;
        private double _progress;
        private double _progressStep;

        public List<IOrionDevice> OnlineDevices { get; set; } = [];

        public C2000M(IPort port) : base(port)
        {
            ModelCode = Code;
            Model = "С2000М";
            SupportedModels =
            [
                Model,
            ];

            _bolidDict = new()
            {
                { 0, () => new C2000M(port) },
                { 1, () => new OrionDevice(port) },          //"Сигнал-20" },
                { 2, () => new Signal20P(port) },        //"Сигнал-20П, Сигнал-20П исп.01" },
                { 3, () => new C2000sp1(port) },         //"С2000-СП1, С2000-СП1 исп.01" },
                { 4, () => new C2000_4(port) },          //"С2000-4" },
                { 7, () => new OrionDevice(port) },          //"С2000-К" },
                { 8, () => new OrionDevice(port) },          //"С2000-ИТ" },
                { 9, () => new C2000_Kdl(port) },        //"С2000-КДЛ" },
                { 10, () => new OrionDevice(port) },         //"С2000-БИ/БКИ" },
                { 11, () => new OrionDevice(port) },         //"Сигнал-20(вер. 02)" },
                { 13, () => new OrionDevice(port) },         //"С2000-КС" },
                { 14, () => new OrionDevice(port) },         //"С2000-АСПТ" },
                { 15, () => new OrionDevice(port) },         //"С2000-КПБ" },
                { 16, () => new C2000_2(port) },         //"С2000-2" },
                { 19, () => new OrionDevice(port) },         //"УО-ОРИОН" },
                { 20, () => new OrionDevice(port) },         //"Рупор" },
                { 21, () => new OrionDevice(port) },         //"Рупор-Диспетчер исп.01" },
                { 22, () => new OrionDevice(port) },         //"С2000-ПТ" },
                { 24, () => new OrionDevice(port) },         //"УО-4С" },
                { 25, () => new OrionDevice(port) },         //"Поток-3Н" },
                { 26, () => new Signal20M(port) },       //"Сигнал-20М" },
                { 28, () => new OrionDevice(port) },         //"С2000-БИ-01" },
                { 29, () => new C2000Ethernet(port) },   //"С2000-Ethernet" },
                { 30, () => new OrionDevice(port) },         //"Рупор-01" },
                { 31, () => new OrionDevice(port) },         //"С2000-Adem" },
                { 33, () => new RipRs12_51(port) },      //"РИП-12 исп.50, РИП-12 исп.51, РИП-12 без исполнения"
                { 34, () => new Signal_10(port) },       //"Сигнал-10" },
                { 36, () => new OrionDevice(port) },         //"С2000-ПП" },
                { 38, () => new RipRs24_54(port) },      //"РИП-12 исп.54" },
                { 39, () => new RipRs24_51(port) },      //"РИП-24 исп.50, РИП-24 исп.51" },
                { 41, () => new C2000_Kdl2i(port) },     //"С2000-КДЛ-2И" },
                { 43, () => new OrionDevice(port) },         //"С2000-PGE" },
                { 44, () => new OrionDevice(port) },         //"С2000-БКИ" },
                { 45, () => new OrionDevice(port) },         //"Поток-БКИ" },
                { 46, () => new OrionDevice(port) },         //"Рупор-200" },
                { 47, () => new C2000Perimeter(port) },  //"С2000-Периметр" },
                { 48, () => new OrionDevice(port) },         //"МИП-12" },
                { 49, () => new OrionDevice(port) },         //"МИП-24" },
                { 53, () => new RipRs_48(port) },        //"РИП-48 исп.01" },
                { 54, () => new RipRs12_56(port) },      //"РИП-12 исп.56" },
                { 55, () => new RipRs24_56(port) },      //"РИП-24 исп.56" },
                { 59, () => new OrionDevice(port) },         //"Рупор исп.02" },
                { 61, () => new OrionDevice(port) },         //"С2000-КДЛ-Modbus" },
                { 66, () => new OrionDevice(port) },         //"Рупор исп.03" },
                { 67, () => new OrionDevice(port) }          //"Рупор-300" }
            };
        }

        public async Task<IOrionDevice> GetNewOnlineDevice(int address)
        {
            var deviceCode = await GetModelCodeAsync((byte)address);
            var deviceFunc = _bolidDict[deviceCode];

            return deviceFunc();
        }

        public async Task<byte> GetFirstFreeAddress(
            int startAddress,
            int attempts,
            Action<IOrionDevice> onDeviceFound,
            Action cleanDeviceFound,
            CancellationToken token)
        {
            if (Port == null)
            {
                throw new ArgumentNullException("port is null!");
            }

            var startAddr = GetValidAddress(startAddress);

            Port.MaxRepetitions = attempts;
            byte address = 0;
            cleanDeviceFound();
            for (byte i = startAddr; i < defaultAddress; i++)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                address = await CheckDevice(onDeviceFound, i);

                if (address == 0)
                    continue;

                break;
            }

            Port.MaxRepetitions = 15;
            return address;
        }

        public async Task<IEnumerable<IOrionDevice>> SearchOnlineDevices(
            int startAddress,
            int attempts,
            Action<IOrionDevice> onDeviceFound, 
            Action<double> progressStatus, 
            CancellationToken token)
        {
            if (Port == null)
            {
                throw new ArgumentNullException("port is null!");
            }

            var startAddr = GetValidAddress(startAddress);

            _progress = 1.0;
            _progressStep = 100.0 / (defaultAddress - startAddr);

            progressStatus(Convert.ToInt32(_progress));

            OnlineDevices.Clear();
            Port.MaxRepetitions = attempts;

            // First of all, we need to check address 127 (default bolid address)
            await CheckDeviceWithProgress(onDeviceFound, progressStatus, (byte)defaultAddress);

            for (byte i = startAddr; i < defaultAddress; i++)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await CheckDeviceWithProgress(onDeviceFound, progressStatus, i);
            }
            Port.MaxRepetitions = 15;
            return OnlineDevices;
        }

        private async Task<byte> CheckDevice(Action<IOrionDevice> onDeviceFound, byte currentAddress)
        {
            byte deviceCode;
            try
            {
                deviceCode = await GetModelCodeAsync(currentAddress);
            }
            catch
            {
                return currentAddress;
            }

            if (_bolidDict.TryGetValue(deviceCode, out var device))
            {
                CreateDevice(onDeviceFound, OnlineDevices, currentAddress, deviceCode);
            }
            else
            {
                Debug.WriteLine($"Device code: {deviceCode} is unrecognized. (Address: {currentAddress})");
            }
            return 0;
        }

        public override async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            return await base.SetupAsync(updateProgressBar, Code);
        }

        private async Task CheckDeviceWithProgress(Action<IOrionDevice> onDeviceFound, Action<double> progressStatus, byte devAddr)
        {
            byte deviceCode;
            try
            {
                deviceCode = await GetModelCodeAsync(devAddr);
            }
            catch
            {
                progressStatus(Convert.ToInt32(_progress));
                _progress += _progressStep;
                return;
            }

            if (_bolidDict.TryGetValue(deviceCode, out var device))
            {
                CreateDevice(onDeviceFound, OnlineDevices, devAddr, deviceCode);
            }
            else
            {
                Debug.WriteLine($"Device code: {deviceCode} is unrecognized. (Address: {devAddr})");
            }
            progressStatus(Convert.ToInt32(_progress));
            _progress += _progressStep;
        }

        private void CreateDevice(Action<IOrionDevice> onDeviceFound, List<IOrionDevice> devices, byte devAddr, byte deviceCode)
        {
            var rs485device = _bolidDict[deviceCode]();
            rs485device.AddressRS485 = devAddr;
            rs485device.Port = Port;
            rs485device.Id = _id;
            onDeviceFound(rs485device);
            devices.Add(rs485device);
            _id++;
        }

        private static byte GetValidAddress(int startAddress)
        {
            if (startAddress < 0)
            {
                return 0;
            }
            if (startAddress > defaultAddress)
            {
                return (byte)defaultAddress;
            }
            return (byte)startAddress;
        }
    }
}
