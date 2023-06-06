using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepository
{
    public interface IRepositoryTeam
    {

        List<Team> getTeamsList();
        bool setListTeams(Team newTeam);
        Team findTeamByTeamName(string teamName);
        bool setTeam(Team newTeam);
    }
}
