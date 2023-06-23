using Contracts.RequestService;
using DomainEntity;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPokemonFinderRepository
    {

        List<MovementsDto> GetListFromFile();
        bool PersistEntity(string serialiceEntity);

        string GetActualPresentation();
    }
}
