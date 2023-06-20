using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotAllowGenerationException:Exception
    {

        public NotAllowGenerationException()
        {

        }


        public NotAllowGenerationException(string msg) : base(msg) { }
    }
}
