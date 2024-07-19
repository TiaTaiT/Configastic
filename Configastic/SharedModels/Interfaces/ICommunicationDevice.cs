namespace Configastic.SharedModels.Interfaces
{
    public interface ICommunicationDevice : IDevice
    {
        /// <summary>
        /// Порт
        /// </summary>
        IPort Port { get; set; }
    }
}
