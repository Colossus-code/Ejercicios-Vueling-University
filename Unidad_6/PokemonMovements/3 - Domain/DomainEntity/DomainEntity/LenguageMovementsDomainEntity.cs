
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
            string toReturn = $"New movements has been introduced at: {IntroducedAt}\n";
            
            
            foreach(var movement in MovementsFound)
            {
                toReturn += $"With the ID: {movement.MoveId} and type by: {movement.MoveType}\n";
                toReturn += $" {movement.MoveLenguage.Select(e => e.MovementNameByLanguage.Values)}";
                toReturn += $" {movement.MoveLenguage.Select(e => e.MovementDescByLanguage.Values)}";
            }

            return toReturn ;

        }
    }
}
