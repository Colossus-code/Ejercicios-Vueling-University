using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository
{
    public interface IRepositoryTeam
    {

        List<Team> getTeamsList();
        bool setListTeams(Team newTeam);
        Team findTeamByTeamName(string teamName);
        bool setTeam(TeamDto newTeam);
    }
}
