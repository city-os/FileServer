using System;
using System.Runtime.Serialization;

namespace CityOs.FileServer.Core.Exceptions
{
    public class NullDirectoryFileStorageException : Exception
    {
        public NullDirectoryFileStorageException()
        {
        }

        protected NullDirectoryFileStorageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NullDirectoryFileStorageException(string message) : base(message)
        {
        }

        public NullDirectoryFileStorageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
