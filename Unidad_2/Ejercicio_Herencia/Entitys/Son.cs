using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Herencia.Entitys
{
    public class Son : Father
    {
        public static int id;
        public string sonName { get; set; }
        protected int sonAge { get; set; }
        public string sonJob { get; set; }

        public Son(string sonName, int sonAge, string valueOfJob, string fatherName, int fatherAge, string fatherJob, string grandFatherName, int grandFatherAge, string grandFathersJob)
            : base(fatherJob, grandFathersJob)
        {
            this.sonName = sonName;
            this.sonAge = sonAge;
            this.sonJob = valueOfJob;

            this.fatherName = fatherName;
            this.fatherAge = fatherAge;

            this.grandFatherName = grandFatherName;
            this.grandFatherAge = grandFatherAge;

        }

        public new void showData()
        {
            base.showData();
            Console.WriteLine($"The son name is:{sonName} \n" +
            $"The son age is:{sonAge} \n" +
            $"The son job is:{sonJob}");

        }

        public void changeValuesOfSon(string sonNewName, int sonNewAge, string sonNewJob, string fatherNewName, int fatherNewAge, string fatherNewJob, string grandfatherNewName, int grandfatherNewAge, string grandfatherNewJob)
        {
            base.changeValuesOfFather(fatherNewJob, grandfatherNewJob);

            this.grandFatherName = grandfatherNewName;
            this.grandFatherAge = grandfatherNewAge;

            this.fatherName = fatherNewName;
            this.fatherAge = fatherNewAge;

            this.sonName = sonNewName;
            this.sonAge = sonNewAge;
            this.sonJob = sonNewJob;


        }
    }
}
