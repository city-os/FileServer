using System;
using System.Runtime.Serialization;

namespace CityOs.FileServer.Core.Exceptions
{
    public class InconsistentMimeTypeException : Exception
    {
        public InconsistentMimeTypeException()
        {
        }

        protected InconsistentMimeTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InconsistentMimeTypeException(string message) : base(message)
        {
        }

        public InconsistentMimeTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
