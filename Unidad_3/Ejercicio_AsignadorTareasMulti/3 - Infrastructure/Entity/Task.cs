

using Ejercicio_AsignadorTareas.Enum;

namespace Ejercicio_AsignadorTareasMulti.Entity
{
    public class Task
    {


        public int TaskId { get; set; } 
        public string TaskDescription { get; set; }
        public string Technology { get; set; }
        public bool Assigned { get; set; }
        public int WorkerId { get; set; }
        public TaskStatus StatusOfTask { get; set; }

    }
}
