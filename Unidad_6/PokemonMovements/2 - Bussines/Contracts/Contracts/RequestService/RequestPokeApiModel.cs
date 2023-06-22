using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.RequestService
{
    public class RequestPokeApiModel
    {
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
    }
}
