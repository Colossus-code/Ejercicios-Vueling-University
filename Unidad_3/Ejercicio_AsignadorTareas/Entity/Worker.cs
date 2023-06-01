using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareas.Entity
{
    public class Worker
    {
        public static int workerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime LeavingDate { get; set; }

        public Worker(string name, string surname, DateTime birthDate)
        {
            Worker.workerId++;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
    }
}
