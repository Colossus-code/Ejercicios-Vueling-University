using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity
{
    public class PasswordEncryptDomainEntity
    {
        public byte[] SaltPassword { get; set; }

        public byte[] HashPassword { get; set; }

    }
}
