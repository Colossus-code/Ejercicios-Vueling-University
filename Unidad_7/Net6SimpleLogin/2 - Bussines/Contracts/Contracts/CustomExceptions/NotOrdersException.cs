using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class NotOrdersException : Exception
    {
        public NotOrdersException()
        {

        }

        public NotOrdersException(string msg) : base(msg) { }
    }
}
