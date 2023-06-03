

using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicio_AsignadorTareasMulti._4___Repository
{
    public class RepositoryTeam : IRepositoryTeam
    {

        private List<Team> _teamsList;

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
                    TeamName = "Equipo Prueba 2"
                },
                new Team()
                {
                    TeamName = "Equipo Prueba 3"
                }
            };
        }

        public Team findTeamByTeamName(string teamName)
        {
            return _teamsList.FirstOrDefault(e => e.TeamName == teamName);
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
    }
}
