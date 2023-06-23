using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class NotEnougthMovementsException : Exception
    {

        public NotEnougthMovementsException()
        {

        }


        public NotEnougthMovementsException(string msg) : base(msg) { }
    }
}
