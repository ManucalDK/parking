using System;
using System.Runtime.Serialization;

namespace AppCore.Exceptions
{
    [Serializable]
    public class DepartureException : Exception
    {
        public DepartureException()
        {
        }

        public DepartureException(string message) : base(message)
        {
        }

        public DepartureException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DepartureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}