using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.Utils;
using System.Net;
using System.Net.Sockets;

namespace Configastic.SharedModels.Models.Ports
{
    public class BolidUdpClient : IPort
    {
        private const int DEFAULT_MAX_REPETITIONS = 15;
        private const int DEFAULT_TIMEOUT = 60;
        private const int ACTUAL_PACKET_LENGTH_INDEX = 1;
        private readonly UdpClient _udpClient;
        private byte _commandCounter = 0x00;

        public int MaxRepetitions { get; set; }

        public IPAddress RemoteServerIp { get; set; } = default!;

        public int RemoteServerUdpPort { get; set; }

        public int ClientUdpPort { get; set; }

        public int Timeout { get; set; }

        public BolidUdpClient(int clientUdpPort)
        {
            ClientUdpPort = clientUdpPort;
            MaxRepetitions = DEFAULT_MAX_REPETITIONS;
            Timeout = DEFAULT_TIMEOUT;
            _udpClient = new UdpClient(ClientUdpPort);
        }

        public byte[] Send(byte[] packet)
        {
            
            var crc = OrionCRC.GetCrc8(packet);
            var data = ArraysHelper.CombineArrays(packet, crc);
            var header = GetBolidUdpHeader(data);
            var complitePacket = ArraysHelper.CombineArrays(header, data);
            int attempts = 0;
            var remoteEndPoint = new IPEndPoint(RemoteServerIp, RemoteServerUdpPort);
            byte[]? receiveBuffer = null;

            while (attempts < MaxRepetitions)
            {
                attempts++;
                _udpClient.Send(complitePacket, complitePacket.Length, remoteEndPoint);
                receiveBuffer = _udpClient.Receive(ref remoteEndPoint);
                if (receiveBuffer != null)
                {
                    break;
                }
            }

            if (receiveBuffer == null)
            {
                throw new TimeoutException("Timeout waiting for server response.");
            }

            var response = BolidUdpClient.GetResponse(receiveBuffer);
            return response;
        }

        public void SendWithoutСonfirmation(byte[] data)
        {
            Send(data);
        }

        /// <summary>
        /// We have to remove Bolid UDP header (second byte = actual response length with CRC8)
        /// </summary>
        /// <param name="receiveBuffer">Full response</param>
        /// <returns>Response with CRC8</returns>
        private static byte[] GetResponse(byte[] receiveBuffer)
        {
            var packetLength = receiveBuffer[ACTUAL_PACKET_LENGTH_INDEX];
            var length = receiveBuffer.Length;
            return receiveBuffer.Skip(length - packetLength).ToArray();
        }

        private byte[] GetBolidUdpHeader(byte[] data)
        {
            var length = (byte)data.Length;
            return [0x10, length, 0x00, _commandCounter, 0x10]; //0x10 0x07 0x00 0x00 0x10
        }

        public void Dispose()
        {
            
        }
    }
}
