using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers.InputValidation
{
    public class InputValidator
    {
        public string validationStringEntry()
        {
            string introducedStringValue = Console.ReadLine();

            if (introducedStringValue.Trim().Equals(""))
            {
                return null;
            }

            return introducedStringValue;
        }

        public int validationIntEntry()
        {
            int introducedIntValue = -1;
            try
            {
                return introducedIntValue = Convert.ToInt32(Console.ReadLine());

            }
            catch (FormatException)
            {
                return introducedIntValue;
            }

        }
        public DateTime validationDateTimeEntry(out bool isParsed)
        {

            isParsed = DateTime.TryParse(Console.ReadLine(), out DateTime dateTime);

            return dateTime;
        }
    }
}

