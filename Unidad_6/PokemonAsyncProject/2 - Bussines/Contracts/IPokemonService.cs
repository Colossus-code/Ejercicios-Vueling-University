using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPokemonService
    {
        Task<string> AddPokemonsFromUrl(List<string> urls);
        Task<string> AddPokemonFromName(string pokemonName);
    }
}
