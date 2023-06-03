
using Ejercicio_AsignadorTareas.Enum;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;

namespace Ejercicio_AsignadorTareasMulti._4___Repository
{
    public class RepositoryTask : IRepositoryTask
    {
        private List<Task> _taskList;
        public RepositoryTask()
        {
            _taskList = new List<Task>()
            {
                new Task()
                {

                    TaskDescription = "Create data base on SQL Server",
                    Assigned = false,
                    Technology = "SQL",
                    StatusOfTask = TaskStatus.todo
                },

                new Task()
                {
                    TaskDescription = "Update file into Web API",
                    Assigned = false,
                    Technology = "C#",
                    StatusOfTask = TaskStatus.todo
                },
                new Task()
                {
                    TaskDescription = "Create PDF with the inform of the month",
                    Assigned = false,
                    Technology = "Java",
                    StatusOfTask = TaskStatus.todo
                }
            };
        }

        public List<Task> getTasks()
        {
            return _taskList;
        }

        public bool setTask(Task newTask)
        {
            try
            {
                _taskList.Add(newTask);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
