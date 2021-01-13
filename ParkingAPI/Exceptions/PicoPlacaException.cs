using System;
using System.Runtime.Serialization;

namespace ParkingAPI.Exceptions
{
    [Serializable]
    internal class PicoPlacaException : Exception
    {
        public PicoPlacaException()
        {
        }

        public PicoPlacaException(string message) : base(message)
        {
        }

        public PicoPlacaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PicoPlacaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}