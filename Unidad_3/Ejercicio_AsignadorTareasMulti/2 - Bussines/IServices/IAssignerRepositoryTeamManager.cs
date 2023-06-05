﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.IServices
{
    public interface IAssignerRepositoryTeamManager
    {
        string assingItWorkerToTeach(int idWorker, string teamName, int idManager);
        string assingTaskToItWorker(int idWorker, int taskID, int idManager);
        string getItWorkersList(int idManager);
        string getTaskId();
    }
}
