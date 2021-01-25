using System;
using System.Runtime.Serialization;

namespace AppCore.Exceptions
{
    [Serializable]
    public class CellException : AppException
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