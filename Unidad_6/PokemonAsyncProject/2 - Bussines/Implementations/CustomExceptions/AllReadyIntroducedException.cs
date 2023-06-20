using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class AllReadyIntroducedException: Exception
    {

        public AllReadyIntroducedException()
        {

        }


        public AllReadyIntroducedException(string msg) : base(msg) { }
    }
}
