using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotAllowTypeException : Exception
    {

        public NotAllowTypeException()
        {

        }


        public NotAllowTypeException(string msg) : base(msg) { }
    }
}
