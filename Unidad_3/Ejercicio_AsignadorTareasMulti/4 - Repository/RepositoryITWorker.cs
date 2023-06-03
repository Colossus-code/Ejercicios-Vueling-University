

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
        private ITWorker _worker; 

        public RepositoryITWorker()
        {
            _itWorkerList = new List<ITWorker>()
            {
                new ITWorker()
                {
                    ItWorkerId = Worker.WorkerId,
                    Name = "Carlos",
                    Surname = "Perez Gonzalez",
                    BirthDate = new DateTime(11 / 03 / 1993),
                    YearsExperiencie = 7,
                    TechKnowledges = new List<string>() { "Java", "C#", "C++" },
                    ItWorkerLevel = ITWorkerLevel.senior,
                    TeamName = "Equipo Prueba 1"
                },
                new ITWorker()
                {
                    ItWorkerId = Worker.WorkerId,
                    Name = "Jacinta",
                    Surname = "Rosales Sanchez",
                    BirthDate = new DateTime(04 / 05 / 1997),
                    YearsExperiencie = 3,
                    TechKnowledges = new List<string>() { "C#", "HTML", "Agular" },
                    ItWorkerLevel = ITWorkerLevel.medium,
                    TeamName = "Equipo Prueba 1"
                },                
                new ITWorker()
                {
                    ItWorkerId = Worker.WorkerId,
                    Name = "Manuel",
                    Surname = "Martinez de la Rosa",
                    BirthDate = new DateTime(21 / 01 / 1994),
                    YearsExperiencie = 5,
                    TechKnowledges = new List<string>() { "C#", "Phyton" },
                    ItWorkerLevel = ITWorkerLevel.medium,
                    TeamName = "Equipo Prueba 1"
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
            return _itWorkerList;
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
            return _itWorkerList.FirstOrDefault(e => e.ItWorkerId == idWorker);
        }
        
        public bool setItWorker(ItWorkerDto newWorker)
        {
            try
            {
                _worker = new ITWorker();
                _worker.Name = newWorker.WorkerName;
                _worker.Surname = newWorker.WorkerSurname;
                _worker.BirthDate = newWorker.WorkerBirthDay;
                _worker.YearsExperiencie = newWorker.WorkerYearsExperience;
                _worker.TechKnowledges = newWorker.Knowledge;
                _worker.ItWorkerLevel = (ITWorkerLevel)newWorker.TechLevel;

                return true;
            
            }catch (Exception)
            {
                return false;
            }
        }
    }
}
