using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configastic.Exceptions
{
    public class InvalidDeviceResponseException : Exception
    {
        public byte[] Response { get; private set; }

        public InvalidDeviceResponseException(byte[] response)
        {
            Response = response;
        }

        public InvalidDeviceResponseException(byte[] response, string message) : base(message)
        {
            Response = response;
        }

        public InvalidDeviceResponseException(byte[] response, string message, Exception innerException) : base(message, innerException)
        {
            Response = response;
        }
    }
}
