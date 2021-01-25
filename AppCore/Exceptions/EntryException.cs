using System;
using System.Runtime.Serialization;

namespace AppCore.Exceptions
{
    [Serializable]
    public class EntryException : AppException
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