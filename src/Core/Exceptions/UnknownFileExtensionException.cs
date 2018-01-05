using System;
using System.Runtime.Serialization;

namespace CityOs.FileServer.Core.Exceptions
{
    public class UnknownFileExtensionException : Exception
    {
        public UnknownFileExtensionException(string message) : base(message)
        {
        }

        public UnknownFileExtensionException()
        {
        }

        protected UnknownFileExtensionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        
        public UnknownFileExtensionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
