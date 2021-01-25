using System;
using System.Runtime.Serialization;

namespace AppCore.Exceptions
{
    [Serializable]
    public class PlacaException : AppException
    {
        public PlacaException()
        {
        }

        public PlacaException(string message) : base(message)
        {
        }

        public PlacaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PlacaException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}