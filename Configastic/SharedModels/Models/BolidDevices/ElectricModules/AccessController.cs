using Configastic.SharedModels.Interfaces;

namespace Configastic.SharedModels.Models.BolidDevices.ElectricModules
{
    public class AccessController(IOrionDevice orionDevice)
    {
        private IOrionDevice _parentDevice = orionDevice;
        private byte accessCommandCode = 0x23;

        public async Task<byte[]> ProvidingAccess()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x00 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> AccessPermission ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x01 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> PermissionEntrance ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x02 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> PermissionOutput ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x03 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> AccessDenied ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x04 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> EntranceDenied ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x05 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> OutputeDenied ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x06 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }

        public async Task<byte[]> AllowAccess ()
        {
            var packet = new byte[] { accessCommandCode, 0x00, 0x07 };
            var result = _parentDevice.AddressTransactionAsync((byte)_parentDevice.AddressRS485, packet, IOrionNetTimeouts.Timeouts.readModel);
            return await result;
        }
    }
}
