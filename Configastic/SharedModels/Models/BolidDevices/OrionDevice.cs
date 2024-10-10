using Configastic.Exceptions;
using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.Base;
using Configastic.SharedModels.Models.Utils;
using System.Diagnostics;
using System.Net;
using static Configastic.SharedModels.Interfaces.IOrionNetTimeouts;

namespace Configastic.SharedModels.Models.BolidDevices
{
    public class OrionDevice : Rs485device, IOrionDevice
    {
        private const int ResponseNewAddressOffset = 2;
        private const int ResponseDeviceModelOffset = 1;
        protected const uint defaultAddress = 127;
        private static byte commandCounter;

        public OrionDevice(IPort port)
        {
            Response = [];
            Port = port;
        }

        //public static int Code;

        public int ModelCode { get; set; }

        public byte[] Response { get; private set; }

        public async Task<bool> ChangeDeviceAddressAsync(byte newDeviceAddress)
        {

            byte[] cmdString = GetChangeAddressPacket(newDeviceAddress);

            var result = await AddressTransactionAsync((byte)AddressRS485, cmdString, Timeouts.addressChanging);

            if (result.Length <= ResponseNewAddressOffset)
            {
                throw new InvalidDeviceResponseException(result, "Device response was not valid or null");
            }
            var success = result[ResponseNewAddressOffset] == newDeviceAddress;

            AddressRS485 = newDeviceAddress;

            return success;
        }

        public async Task<bool> SetAddressAsync()
        {
            byte[] cmdString = GetChangeAddressPacket((byte)AddressRS485);

            var result = await AddressTransactionAsync((byte)defaultAddress, cmdString, Timeouts.addressChanging);

            if (result == null || result?.Length <= ResponseNewAddressOffset)
                return false;

            byte? success = result?[ResponseNewAddressOffset];
            if (success == null)
                return false;

            return success == (byte)AddressRS485;
        }

        public async Task RebootAsync()
        {
            if (Port == null)
                return;

            byte[] completePacket = GetCompletePacket((byte)AddressRS485, GetRebootPacket());
            Port.Timeout = (int)Timeouts.readModel;
            await Port.SendWithoutСonfirmationAsync(completePacket);
        }

        private byte[] GetRebootPacket()
        {
            return
            [
                (byte)OrionCommands.Reboot,
                0x00,
                0x00,
            ];
        }

        private byte[] GetChangeAddressPacket(byte address)
        {
            // формируем команду на отправку
            return
            [
                (byte)OrionCommands.ChangeAddress,
                address,
                address
            ];
        }

        public async Task<bool> IsDeviceOnlineAsync()
        {
            if (Port == null) 
                return false;

            Port.MaxRepetitions = 3;
            try
            {
                var result = await GetModelCodeAsync((byte)AddressRS485);
                Port.MaxRepetitions = 15;
                if (result != ModelCode)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<byte> GetModelCodeAsync(byte deviceAddress)
        {
            // формируем команду на отправку
            var cmdString = new byte[] { (byte)OrionCommands.GetModel, 0x00, 0x00 };
            
            byte deviceCode;

            Response = await AddressTransactionAsync(deviceAddress, cmdString, Timeouts.readModel);

            if (Response.Length == 0)
                throw new DeviceIsOfflineException($"Device with address {deviceAddress} is offline");
                
            deviceCode = Response[ResponseDeviceModelOffset];
            return deviceCode;
        }

        public virtual Task WriteBaseConfigAsync(Action<int> progressStatus)
        {
            return Task.CompletedTask;
        }

        public virtual Task WriteConfigAsync(Action<int> progressStatus)
        {
            return Task.CompletedTask;
        }

        public async Task<byte[]> AddressTransactionAsync(byte address,
                                         byte[] sendArray,
                                         IOrionNetTimeouts.Timeouts timeout)
        {
            if (Port == null)
            {
                throw new ArgumentNullException("Port");
            }

            byte[] completePacket = GetCompletePacket(address, sendArray);
            Port.Timeout = (int)timeout;
            var response = await Port.SendAsync(completePacket);

            return GetResponseWithoutAuxiliaryData(response);
        }

        private byte[] GetCompletePacket(byte address, byte[] sendArray)
        {
            var addr = new[] { address };
            var sendCommand = ArraysHelper.CombineArrays(addr, sendArray);
            var completePacket = OrionDevice.GetComplitePacket(sendCommand);
            return completePacket;
        }

        private static byte[] GetResponseWithoutAuxiliaryData(byte[] responseArray)
        {
            var response = responseArray.ToList();
            if (responseArray.Length < 2)
                return responseArray;
            // Удаляем последний байт (CRC8)
            response.RemoveAt(response.Count - 1);
            // Удаляем первый байт (Адрес ответившего устройства)
            response.RemoveAt(0);
            // Удаляем второй байт (Длина посылки)
            response.RemoveAt(1);

            return [.. response];
        }

        private static byte[] GetComplitePacket(byte[] sendArray)
        {
            byte bytesCounter = 2; //сразу начнём считать с двойки, т.к. всё равно придётся добавить два байта(сам байт длины команды, и счётчик команд)
            var lst = new List<byte>();
            foreach (var bt in sendArray)
            {
                lst.Add(bt);
                bytesCounter++;
            }

            lst.Insert(1, bytesCounter); //вставляем вторым байтом в пакет длину всего пакета + байт длины пакета
            lst.Insert(2, commandCounter); //вставляем третьим байтом в пакет счётчик команд
            var cmd = lst.ToArray();

            commandCounter += (byte)(bytesCounter + 0x01); // увеличиваем счётчик комманд на кол-во отправленных байт

            return cmd;
        }

        public virtual async Task<bool> SetupAsync(Action<int> updateProgressBar, int modelCode = 0)
        {
            try
            {
                byte deviceCode = await GetModelCodeAsync((byte)defaultAddress);
                if (deviceCode != ModelCode)
                    return false;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            await SetAddressAsync();

            byte deviceCodeWithNewAddress;

            try
            {
                deviceCodeWithNewAddress = await GetModelCodeAsync((byte)AddressRS485);
                if (deviceCodeWithNewAddress != ModelCode)
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            await WriteBaseConfigAsync(updateProgressBar);

            return true;
        }

        public async Task<bool> CheckDeviceTypeAsync()
        {
            try
            {
                var result = await GetModelCodeAsync((byte)AddressRS485);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
