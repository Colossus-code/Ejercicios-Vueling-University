using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotFoundFireOrGeneration : Exception
    {

        public NotFoundFireOrGeneration()
        {

        }


        public NotFoundFireOrGeneration(string msg) : base(msg) { }
    }
}
