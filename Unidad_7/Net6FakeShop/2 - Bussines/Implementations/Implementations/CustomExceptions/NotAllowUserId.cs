using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations.CustomExceptions
{
    public class NotAllowUserId : Exception
    {

        public NotAllowUserId() { }

        public NotAllowUserId(string message) : base(message) { }
    
    }
}
