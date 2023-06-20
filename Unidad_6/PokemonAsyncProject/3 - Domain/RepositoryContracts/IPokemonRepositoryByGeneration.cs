using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPokemonRepositoryByGeneration
    {

        Task<PokemonDomainEntity> GetPokemonFromApi(string url);

        bool PersistPokemon(PokemonDomainEntity pokemon, int generation);
    }
}
