using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPokemonRepositoryUrls
    {
        bool PersistPokemonAsJson(List<PokemonDomainEntity> pokemon);

        Task<List<PokemonDomainEntity>> GetAllIntroduced(List<string> urls);
    }
}
