using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Entity
{
    internal class Task
    {
        public static int taskIdIterator;
        public int taskId { get; set; }
        public string taskDescription { get; set; }
        public string technology { get; set; }
        public bool assigned { get; set; }
        public ITWorker worker { get; set; }
        public enum status
        {
            todo,
            doing,
            done
        }

        public Task(string taskDescription, string technology, status taskStatus = status.todo)
        {
            taskIdIterator++;
            this.assigned = false;
            this.taskId = taskIdIterator;
            this.taskDescription = taskDescription;
            this.technology = technology;

        }
    }
}
