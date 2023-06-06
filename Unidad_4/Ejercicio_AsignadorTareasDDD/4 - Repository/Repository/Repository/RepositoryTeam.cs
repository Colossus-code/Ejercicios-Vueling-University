using Infrastructure.Entity;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryTeam : IRepositoryTeam
    {

        private List<Team> _teamsList;
        private Team _newTeam;

        public RepositoryTeam()
        {
            _teamsList = new List<Team>()
            {
                new Team()
                {
                    TeamName = "Equipo Prueba 1",
                    ManagerTeamId = 1,
                    TechnicianId = {2,3}
                },
                new Team()
                {
                    TeamName = "Equipo Prueba 2",
                    ManagerTeamId = -1,
                },
                new Team()
                {
                    TeamName = "Equipo Prueba 3",
                    ManagerTeamId = -1,
                }
            };
        }

        public Team findTeamByTeamName(string teamName)
        {
            try
            {
                return _teamsList.FirstOrDefault(e => e.TeamName == teamName);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<Team> getTeamsList()
        {
            return _teamsList;
        }

        public bool setListTeams(Team newTeam)
        {
            try
            {
                _teamsList.Add(newTeam);
                return true;

            }
            catch (Exception)
            {
                return false;
            }


        }

        public bool setTeam(Team newTeam)
        {
            try
            {
                _newTeam = new Team();
                _newTeam = newTeam;
                _newTeam.ManagerTeamId = -1;
                _teamsList.Add(_newTeam);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
