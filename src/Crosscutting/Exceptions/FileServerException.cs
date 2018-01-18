using System;
using System.Runtime.Serialization;

namespace CityOs.FileServer.Crosscutting.Exceptions
{
    public class FileServerException : Exception
    {
        public FileServerException()
        {
        }

        public FileServerException(string message) : base(message)
        {
        }

        public FileServerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileServerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
