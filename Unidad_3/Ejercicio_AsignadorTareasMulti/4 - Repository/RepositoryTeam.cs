

using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
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

        public bool setTeam(TeamDto newTeam)
        {
            try
            {
                _newTeam = new Team();
                _newTeam.TeamName = newTeam.TeamName;

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
