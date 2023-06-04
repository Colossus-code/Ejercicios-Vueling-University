using Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation;
using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository
{
    public interface IRepositoryTask
    {
        List<Task> getTasks();

        bool setTaskList(Task newTask);

        bool setTask(TaskDto newTask);

        Task getTaskById(int taskId);
    }
}
