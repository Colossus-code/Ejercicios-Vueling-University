using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Herencia.Entitys
{
    public class GrandFather
    {
        public string grandFatherName { get; set; }
        protected int grandFatherAge { get; set; }
        public string grandFatherJob { get; set; }

        public GrandFather(string valueOfJob)
        {
            this.grandFatherJob = valueOfJob;
        }

        public virtual void showData()
        {
            Console.WriteLine($"The grandfather name is:{grandFatherName} \n" +
                $"The grandfather age is:{grandFatherAge} \n" +
                $"The grandfather job is:{grandFatherJob}");

        }

        public void changeValuesOfGrandFather(string newJobValue)
        {
            this.grandFatherJob = newJobValue;

        }
    }
}
