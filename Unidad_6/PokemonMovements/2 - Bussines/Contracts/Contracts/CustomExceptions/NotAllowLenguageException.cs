using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class NotAllowLenguageException : Exception
    {

        public NotAllowLenguageException()
        {

        }


        public NotAllowLenguageException(string msg) : base(msg) { }
    }
}

