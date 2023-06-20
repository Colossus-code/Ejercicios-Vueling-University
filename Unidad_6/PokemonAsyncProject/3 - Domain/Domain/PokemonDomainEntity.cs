using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PokemonDomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Abilities { get; set; }
        public List<string> Types { get; set; }
        public string Generation { get; set; }


        public override string ToString()
        {

            string returnObjectToString = $"**_introduced {Name} of the {Generation}, with abilities, ";

            foreach (string abilitie in Abilities)
            {

                returnObjectToString += $"{abilitie}, ";
            }

            returnObjectToString += $"at {DateTime.UtcNow}.";

            return returnObjectToString;
        }
    }
}
