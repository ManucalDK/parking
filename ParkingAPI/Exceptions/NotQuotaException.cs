using System;
using System.Runtime.Serialization;

namespace ParkingAPI.Exceptions
{
    [Serializable]
    internal class NotQuotaException : Exception
    {
        public NotQuotaException()
        {
        }

        public NotQuotaException(string message) : base(message)
        {
        }

        public NotQuotaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotQuotaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}