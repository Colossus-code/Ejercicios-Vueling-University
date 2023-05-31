using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_Colecciones
{
    internal class ColeccionesClase
    {
        public enum Days
        {

            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday

        }

        public class EjercicioOptativo
        {
            //Array of numbers 
            public int[] numericArray = new int[] { 12, 38, 23, 105, 84, 57 };
            //List of strings
            public List<string> encodedMessage = new List<string>();
            //Dictionary Key/Value name/age
            public Dictionary<string, int> nameAndAge = new Dictionary<string, int>();

            //Class constructor
            public EjercicioOptativo()
            {

                encodedMessage.Add("This");
                encodedMessage.Add("Is");
                encodedMessage.Add("The message");

                nameAndAge.Add("Carlos", 21);
                nameAndAge.Add("Maria", 44);
                nameAndAge.Add("Manuel", 28);

                printCollections();
                printVars();

            }

            // This prints the information wich inside of the collections 
            #region CollectionRegion
            public void printCollections()
            {
                // For run away the array let's make a for

                for (int i = 0; i < numericArray.Length; i++)
                {
                    Console.Write(numericArray[i] + " ");
                }

                Console.WriteLine("\n");
                // Instead of normal for using a foreach

                foreach (var e in encodedMessage)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("\n");

                foreach (var e in nameAndAge)
                {
                    Console.WriteLine($"The name is: {e.Key} and the age is {e.Value}");
                    Console.WriteLine("Another way to make the same {0} the age is {1}", e.Key, e.Value + "\n");

                }

            }
            #endregion

            //This prints the information of the enum, dynamic variable and var
            #region Variables region
            public void printVars()
            {

                //Get in a Days variable today the enum value wich we want to show in console
                Days today = Days.Thursday;
                Console.WriteLine("Today is: " + today);

                //The var value type is assigned in the compilation runtime, in this case we can change because we given a Object type 
                var thisIsMyVar = new Object();

                Console.WriteLine(thisIsMyVar);

                thisIsMyVar = "Giving value";

                Console.WriteLine(thisIsMyVar);

                thisIsMyVar = 18;

                Console.WriteLine(thisIsMyVar);

                thisIsMyVar = false;

                Console.WriteLine(thisIsMyVar);

                /*
                 *
                 * if we use -> var myVar = "Hello Word"; and then change to myVar = false; this gives us to a compile error
                 *
                 */

                // In this case var object and dynamic works like similar, if var wasn't a object type we can't give var a different type
                dynamic var = 11;

                Console.WriteLine(var);

                var = "Hola mundo";

                Console.WriteLine(var);

                var = false;

                Console.WriteLine(var);

            }
        }
    } 
}
#endregion
