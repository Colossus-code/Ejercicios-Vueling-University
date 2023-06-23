
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntity
{
    public class LenguageMovementsDomainEntity
    {
        public DateTime IntroducedAt = DateTime.UtcNow;

        public List<MovementsDomainEntity> MovementsFound {  get; set; }

        public override string ToString()
        {
            string toReturn = $"\nNew movements has been introduced at: {IntroducedAt}\n";
            
            
            foreach(var movement in MovementsFound)
            {
                toReturn += $"With the ID: {movement.MoveId} and type by: {movement.MoveType}\n";
                toReturn += $" {movement.MoveLenguage.MovementNameByLanguage}";
                toReturn += $" {movement.MoveLenguage.MovementDescByLanguage}";
            }

            return toReturn ;

        }
    }
}
