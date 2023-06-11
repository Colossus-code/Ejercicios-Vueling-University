using Bussines.DataTransferObject;
using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services
{
    public class UnitsService : IUnitsService
    {
        private readonly IRepositoryUnitsProfile _repositoryUnitsProfiles;
        public UnitsService(IRepositoryUnitsProfile repoUnits)
        {
            _repositoryUnitsProfiles = repoUnits;
        }

        public List<UnitsProfileDto> FindUnitsProfiles(string race)
        {
            List<UnitProfile> unitsForRaces = _repositoryUnitsProfiles.FindUnitsByRace(race);
            List<UnitsProfileDto> unitsForRacesDto = new List<UnitsProfileDto>();

            try
            {
                
                foreach(var unitForRace in  unitsForRaces)
                {
                    
                    UnitsProfileDto unitProfileDto = new UnitsProfileDto
                    {
                        UnitName = unitForRace.UnitName,
                        Strength = unitForRace.Strength,
                        Resistance = unitForRace.Resistance,
                        Attacks = unitForRace.Attacks,
                        Lives = unitForRace.Lives,
                        Race = unitForRace.Race,

                    };

                  
                    unitsForRacesDto.Add(unitProfileDto);
                }
            }
            catch (Exception) {

                return null; 
            
            }

            return unitsForRacesDto; 


        }
    }
}
