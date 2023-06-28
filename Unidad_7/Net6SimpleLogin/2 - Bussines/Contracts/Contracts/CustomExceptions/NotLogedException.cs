using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class NotLogedException : Exception
    {
        public NotLogedException()
        {

        }

        public NotLogedException(string msg) : base(msg) { }
    }
}
