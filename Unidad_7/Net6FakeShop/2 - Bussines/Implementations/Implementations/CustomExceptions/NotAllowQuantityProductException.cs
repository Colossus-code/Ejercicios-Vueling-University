using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotAllowQuantityProductException : Exception
    {

        public NotAllowQuantityProductException() { }

        public NotAllowQuantityProductException(string message) : base(message) { }
    }
}
