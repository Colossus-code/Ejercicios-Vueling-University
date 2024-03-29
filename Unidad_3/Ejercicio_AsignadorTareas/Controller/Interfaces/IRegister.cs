﻿using Ejercicio_AsignadorTareas.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Ejercicio_AsignadorTareas.Entity.Task;

namespace Ejercicio_AsignadorTareas.Controller.Interfaces
{
    internal interface IRegister
    {
        Task registNewTask();

        Team registNewTeam();

        ITWorker registNewWorker();

        List<String> registKnowledges(List<String> knowledge);
    }
}
