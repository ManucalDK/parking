using System;
using System.Runtime.Serialization;

namespace ParkingAPI.Exceptions
{
    [Serializable]
    internal class EntryException : Exception
    {
        public EntryException()
        {
        }

        public EntryException(string message) : base(message)
        {
        }

        public EntryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}