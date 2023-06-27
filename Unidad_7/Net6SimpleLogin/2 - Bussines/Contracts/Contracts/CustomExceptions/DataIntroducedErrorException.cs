using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class DataIntroducedErrorException : Exception
    {
        public DataIntroducedErrorException()
        {
            
        }

        public DataIntroducedErrorException(string msg) : base(msg) { }

    }
}
