using Infrastructure.DomainEntity;
using Infrastructure.IRepository;
using Services.Contracts;
using Services.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services
{
    public class UnitsForArmyService : IUnitsForArmyService
    {
        private readonly IRepositoryUnitsForArmy _unitsForArmyRepository;

        public UnitsForArmyService(IRepositoryUnitsForArmy unitsForArmyRepo)
        {
            _unitsForArmyRepository = unitsForArmyRepo;
        }

        public (string,bool) AddUnitsToArmy (UnitsForArmyDto unitsForArmyDto)
        {

            Army domainEntityArmy = new Army
            {
                ArmyName = unitsForArmyDto.Army.ArmyName
            };

            UnitProfile domainEntityUnitProfile = new UnitProfile
            {
                UnitName = unitsForArmyDto.Units.UnitName
            };

            UnitsForArmy unitsForArmyDomainEntity = new UnitsForArmy
            {
                ArmyId = domainEntityArmy,
                UnitProfilesId = domainEntityUnitProfile,
                CuantityOfUnits = unitsForArmyDto.CuantityOfUnits
            };

            return _unitsForArmyRepository.AddUnitsForArmy(unitsForArmyDomainEntity);
        }
    }
}
