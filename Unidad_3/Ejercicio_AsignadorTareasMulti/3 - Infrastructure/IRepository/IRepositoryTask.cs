using Ejercicio_AsignadorTareasMulti.Entity;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Ejercicio_AsignadorTareasMulti._3___Infrastructure.IRepository
{
    public interface IRepositoryTask
    {
        List<Task> getTasks();

        bool setTask(Task newTask);


    }
}
