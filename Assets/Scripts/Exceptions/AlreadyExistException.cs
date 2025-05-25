using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War.io.Exceptions
{
    public class AlreadyExistException : Exception
    {
        private const string BaseMessage = "This element already exist in collection!";
        public AlreadyExistException() : base(BaseMessage) { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
    }
}
