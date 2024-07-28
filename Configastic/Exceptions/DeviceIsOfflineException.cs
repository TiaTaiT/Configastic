namespace Configastic.Exceptions
{
    public class DeviceIsOfflineException : Exception
    {
        public DeviceIsOfflineException() { }

        public DeviceIsOfflineException(string message) : base(message)
        {
        }

        public DeviceIsOfflineException(string message, Exception innerException) : base(message, innerException) 
        {
        }
    }
}
