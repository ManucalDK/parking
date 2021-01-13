using System;
using System.Runtime.Serialization;

namespace ParkingAPI.Exceptions
{
    [Serializable]
    internal class CCException : Exception
    {
        public CCException()
        {
        }

        public CCException(string message) : base(message)
        {
        }

        public CCException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CCException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}