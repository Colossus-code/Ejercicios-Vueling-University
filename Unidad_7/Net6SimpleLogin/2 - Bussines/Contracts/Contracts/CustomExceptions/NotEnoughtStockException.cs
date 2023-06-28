using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomExceptions
{
    public class NotEnoughtStockException : Exception
    {
        public NotEnoughtStockException()
        {

        }

        public NotEnoughtStockException(string msg) : base(msg) { }
    }
}
