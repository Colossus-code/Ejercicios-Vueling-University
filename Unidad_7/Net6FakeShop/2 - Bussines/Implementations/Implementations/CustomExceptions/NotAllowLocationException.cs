using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotAllowLocationException : Exception
    {

        public NotAllowLocationException() { }

        public NotAllowLocationException(string message) : base(message) { }
    }
}
