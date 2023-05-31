using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Herencia.Entitys
{
    public class Father : GrandFather
    {
        public string fatherName { get; set; }
        protected int fatherAge { get; set; }
        public string fatherJob { get; set; }

        public Father(string valueOfJob, string grandFathersJob) : base(grandFathersJob)
        {
            this.fatherJob = valueOfJob;
        }

        public new void showData()
        {
            base.showData();
            Console.WriteLine($"The father name is:{fatherName} \n" +
            $"The father age is:{fatherAge} \n" +
            $"The father job is:{fatherJob}");

        }

        public void changeValuesOfFather(string newJobValue, string newJobValueGrandFather)
        {
            base.changeValuesOfGrandFather(newJobValueGrandFather);
            this.fatherJob = newJobValue;

        }
    }
}
