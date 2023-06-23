using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class NotRealTypeException : Exception
    {

        public NotRealTypeException()
        {

        }


        public NotRealTypeException(string msg) : base(msg) { }
    }
}
