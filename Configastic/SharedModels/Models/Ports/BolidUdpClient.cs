using Configastic.SharedModels.Interfaces;
using Configastic.SharedModels.Models.Utils;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Configastic.SharedModels.Models.Ports
{
    public class BolidUdpClient : IPort
    {
        private const int ACTUAL_PACKET_LENGTH_INDEX = 1;
        private readonly UdpClient _udpClient;
        private byte _commandCounter = 0x01;

        public int MaxRepetitions { get; set; } = 15;

        public int ReceiveTimeout { get; set; } = 100;

        public IPAddress RemoteServerIp { get; set; } = new IPAddress([192, 168, 127, 254]);

        public int RemoteServerUdpPort { get; set; }

        public int ClientUdpPort { get; set; }

        public int Timeout { get; set; } = 100;

        public BolidUdpClient(int clientUdpPort)
        {
            ClientUdpPort = clientUdpPort;
        }

        public async Task<byte[]> SendAsync(byte[] packet)
        {
            using (var udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, ClientUdpPort))) // Bind to any available local port
            {
                try
                {
                    byte[] complitePacket = GetCompleteUdpPacket(packet);
                    var serverEndPoint = new IPEndPoint(RemoteServerIp, RemoteServerUdpPort);

                    for (int attempt = 1; attempt <= MaxRepetitions; attempt++)
                    {
                        // Send the message
                        await udpClient.SendAsync(complitePacket, complitePacket.Length, serverEndPoint);

                        // Wait for the acknowledgment with timeout
                        var receiveTask = ReceiveWithTimeoutAsync(udpClient, ReceiveTimeout);
                        var result = await receiveTask;

                        if (result.Received)
                        {
                            // Received a response
                            var response = result.Buffer;
                            return response; // Success, exit the method
                        }
                        else
                        {
                            // Timeout occurred
                            if (attempt < MaxRepetitions)
                            {
                                await Task.Delay(Timeout);
                            }
                            else
                            {
                                return [];
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return [];
        }

        private byte[] GetCompleteUdpPacket(byte[] packet)
        {
            var crc = OrionCRC.GetCrc8(packet);
            var data = ArraysHelper.CombineArrays(packet, crc);
            var header = GetBolidUdpHeader(data);
            var complitePacket = ArraysHelper.CombineArrays(header, data);
            return complitePacket;
        }

        private async Task<(bool Received, byte[] Buffer, IPEndPoint RemoteEndPoint)> ReceiveWithTimeoutAsync(UdpClient client, int timeout)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                try
                {
                    var result = await client.ReceiveAsync(cts.Token);
                    return (true, result.Buffer, result.RemoteEndPoint);
                }
                catch (OperationCanceledException)
                {
                    // Timeout occurred
                    return (false, null, null);
                }
            }
        }

        public async Task SendWithoutСonfirmationAsync(byte[] data)
        {
            await SendAsync(data);
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
            _udpClient?.Dispose();
        }
    }
}
