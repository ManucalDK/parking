using System;
using System.Runtime.Serialization;

namespace ParkingAPI.Exceptions
{
    [Serializable]
    internal class CellException : Exception
    {
        public CellException()
        {
        }

        public CellException(string message) : base(message)
        {
        }

        public CellException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CellException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}