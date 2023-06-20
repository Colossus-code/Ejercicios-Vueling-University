using Domain;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPokemonFinderAndTransform
    {
        Task<PokemonDto> GetPokemonsFromApiUrlAsString(string url);

        PokemonDomainEntity TrasnfromDtoToEntity(PokemonDto pokemonDto);
        PokemonDto TrasnfromEntityToDto(PokemonDomainEntity pokemonDomain);

        string GetAllList(int gen);
    }
}
