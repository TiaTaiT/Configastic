﻿namespace Configastic.SharedModels.Interfaces
{
    public interface IPort : IDisposable
    {
        /// <summary>
        /// The maximum number of attempts to send a message
        /// </summary>
        int MaxRepetitions { get; set; }

        /// <summary>
        /// Send timeout
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// Send data to the port and get response
        /// </summary>
        /// <param name="data">Raw translate data</param>
        /// <returns>Raw response from receiver</returns>
        Task<byte[]> SendAsync(byte[] data);

        /// <summary>
        /// Send data to the port without confirmation from device
        /// </summary>
        /// <param name="data">Raw translate data</param>
        Task SendWithoutСonfirmationAsync(byte[] data);

        /// <summary>
        /// Port initialization
        /// </summary>
        /// <returns>true if initialisation has campleted, false otherwise</returns>
        bool Init();
    }
}
