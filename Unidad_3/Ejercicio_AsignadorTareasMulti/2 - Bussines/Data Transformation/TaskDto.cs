using Ejercicio_AsignadorTareas.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ejercicio_AsignadorTareasMulti._2___Bussines.Data_Transformation
{
    public class TaskDto
    {
        public string TaskDescription { get; set; }
        public string Technology { get; set; }
        public bool Assigned { get; set; }
        public int WorkerId { get; set; }
        public TaskStatus StatusOfTask { get; set; }

        public TaskDto(string taskDesc, string taskTechnology, TaskStatus taskStatus , bool assigned = false)
        {
            TaskDescription = taskDesc;
            Technology = taskTechnology;
            Assigned = assigned;
            StatusOfTask = taskStatus;
        }
    }
}
