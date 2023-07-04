using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotFoundProductsExcepion : Exception
    {
        public NotFoundProductsExcepion() { }

        public NotFoundProductsExcepion(string message) : base(message) { }
    }
}
