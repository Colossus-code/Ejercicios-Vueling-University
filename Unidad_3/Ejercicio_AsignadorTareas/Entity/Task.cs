using Ejercicio_AsignadorTareas.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ejercicio_AsignadorTareas.Entity
{
    public class Task
    {
        public static int taskIdIterator;
        public int taskId { get; set; }
        public string taskDescription { get; set; }
        public string technology { get; set; }
        public bool assigned { get; set; }  
        public ITWorker worker { get; set; }
        public TaskStatus StatusOfTask { get; set; }
        public Task(string taskDescription, string technology, TaskStatus taskStatusVal = TaskStatus.todo)
        {
            taskIdIterator++;
            this.assigned = false;
            this.taskId = taskIdIterator;
            this.taskDescription = taskDescription;
            this.technology = technology;
            StatusOfTask = taskStatusVal;

        }
    }
}
