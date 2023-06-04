
using Ejercicio_AsignadorTareas.Enum;
using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicio_AsignadorTareasMulti._4___Repository
{
    public class RepositoryTask : IRepositoryTask
    {
        private List<Task> _taskList;
        private Task _newTask;

        private static int iteratorId = 4;
        public RepositoryTask()
        {
            _taskList = new List<Task>()
            {
                new Task()
                {
                    TaskId = 1,
                    TaskDescription = "Create data base on SQL Server",
                    Assigned = true,
                    Technology = "SQL",
                    StatusOfTask = TaskStatus.todo
                },

                new Task()
                {
                    TaskId = 2,
                    TaskDescription = "Update file into Web API",
                    Assigned = true,
                    Technology = "C#",
                    StatusOfTask = TaskStatus.doing
                },
                new Task()
                {
                    TaskId = 3,
                    TaskDescription = "Create PDF with the inform of the month",
                    Assigned = true,
                    Technology = "Java",
                    StatusOfTask = TaskStatus.doing
                },
                new Task()
                {
                    TaskId = 4,
                    TaskDescription = "Create this program :D",
                    Assigned = false,
                    Technology = "C#",
                    StatusOfTask = TaskStatus.todo
                }
            };
        }
        public List<Task> getTasks()
        {
            return _taskList;
        }
        public bool setTaskList(Task newTask)
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
        public bool setTask(TaskDto newTask)
        {

            try
            {
                _newTask = new Task();
                _newTask.TaskId = ++iteratorId;
                _newTask.TaskDescription = newTask.TaskDescription;
                _newTask.Technology = newTask.Technology;
                _newTask.StatusOfTask = newTask.StatusOfTask;

                _taskList.Add(_newTask);
                return true;

            }catch(Exception)
            {
                return false;
            }
        }
        public Task getTaskById(int taskId)
        {
            try
            {
                return _taskList.FirstOrDefault(e => e.TaskId == taskId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
