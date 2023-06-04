

using Ejercicio_AsignadorTareas.Enum;
using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicio_AsignadorTareasMulti._4___Repository
{
    public class RepositoryITWorker : IRepositoryITWorker
    {

        private List<ITWorker> _itWorkerList;
        private ITWorker _newWorker;

        private static int iteratorId = 3;

        public RepositoryITWorker()
        {
            _itWorkerList = new List<ITWorker>()
            {
                new ITWorker()
                {
                    ItWorkerId = 1,
                    Name = "Carlos",
                    Surname = "Perez Gonzalez",
                    BirthDate = new DateTime(11 / 03 / 1993),
                    YearsExperiencie = 7,
                    TechKnowledges = new List<string>() { "Java", "C#", "C++", "SQL"},
                    ItWorkerLevel = ITWorkerLevel.senior,
                    TeamName = "Equipo Prueba 1",
                    ItWorkerTaskId = 1
                },
                new ITWorker()
                {
                    ItWorkerId = 2,
                    Name = "Jacinta",
                    Surname = "Rosales Sanchez",
                    BirthDate = new DateTime(04 / 05 / 1997),
                    YearsExperiencie = 3,
                    TechKnowledges = new List<string>() { "C#", "HTML", "Agular" },
                    ItWorkerLevel = ITWorkerLevel.medium,
                    TeamName = "Equipo Prueba 1",
                    ItWorkerTaskId = 3
                },                
                new ITWorker()
                {
                    ItWorkerId = 3,
                    Name = "Manuel",
                    Surname = "Martinez de la Rosa",
                    BirthDate = new DateTime(21 / 01 / 1994),
                    YearsExperiencie = 5,
                    TechKnowledges = new List<string>() { "C#", "Phyton", "SQL"},
                    ItWorkerLevel = ITWorkerLevel.senior,
                    TeamName = "Equipo Prueba 1",
                    ItWorkerTaskId = 2
                },
                new ITWorker()
                {
                    ItWorkerId = 0,
                    Name = "Sergio",
                    Surname = "Garcia Martin",
                    BirthDate = new DateTime(25 / 05 / 2000),
                    YearsExperiencie = 1,
                    TechKnowledges = new List<string>() { "C#", "Java" },
                } 
            };
        }
            
        public List<ITWorker> getItWorkerList() 
        {
            return _itWorkerList.Where(e => e.ItWorkerId > 0).ToList();
        }
        
        public bool setListItWorker(ITWorker newWorker)
        {
            try
            {
                _itWorkerList.Add(newWorker);
                return true;
            
            }
            catch (Exception)
            {
                return false;
            }
            
            
        }

        public ITWorker getWorkerById(int idWorker)
        {
            try
            {
                return _itWorkerList.FirstOrDefault(e => e.ItWorkerId == idWorker);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public bool setItWorker(ItWorkerDto newWorker)
        {
            try
            {               
                _newWorker = new ITWorker();
                _newWorker.ItWorkerId = ++iteratorId;
                _newWorker.Name = newWorker.WorkerName;
                _newWorker.Surname = newWorker.WorkerSurname;
                _newWorker.BirthDate = newWorker.WorkerBirthDay;
                _newWorker.YearsExperiencie = newWorker.WorkerYearsExperience;
                _newWorker.TechKnowledges = newWorker.Knowledge;
                _newWorker.ItWorkerLevel = (ITWorkerLevel)newWorker.TechLevel;

                setListItWorker(_newWorker);
                return true;
            
            }catch (Exception)
            {
                return false;
            }
        }

        public List<ITWorker> getWorkersByTeamName(string teamName)
        {
            try
            {
                return _itWorkerList.Where(e => e.TeamName.Equals(teamName)).ToList();
            
            }catch(Exception) { return null; }
        }
    }
}
