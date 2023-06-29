using Domain.DomainContracts;
using Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.DomainServices
{
    public static class DomainHelper
    {
        public static bool ValidateId(string id)
        {
            Regex expression = new Regex("[A-Z]{2}|[A-Z][0-9]{2}");

            return expression.IsMatch(id);
        }

        public static int RemoveInvalidElements<T>(List<T> list) where T : IValidatable
        {
            //int counter = 0;

            //List<IValidatable> filtredDomain = new List<IValidatable>();

            //foreach (var item in list)
            //{
            //    if (item.Validate())
            //    {
            //        filtredDomain.Add(item);
            //    }
            //    counter++;
            //}

            //list = filtredDomain;

            return list.RemoveAll(e => !e.Validate());
        }
    }

}

