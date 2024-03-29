﻿using Bussines.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class PrinterMenuOptions : IPrinterMenuOptions
    {

        public string menuToPrint(string rolSelected)
        {
            switch (rolSelected)
            {
                case "rolAdmin":

                    return ("\n****************************************************************\n" +
                           "                          ADMIN MODE                               \n" +
                           "___________________________________________________________________\n" +
                           "1. Register new IT worker. \n" +
                           "2. Register new team. \n" +
                           "3. Register new task. \n" +
                           "4. List all team names. \n" +
                           "5. List team members by team name. \n" +
                           "6. List unassigned tasks. \n" +
                           "7. List task assignments by team name. \n" +
                           "8. Assign IT worker to a team as manager. \n" +
                           "9. Assign IT worker to a team as technician. \n" +
                           "10. Assign task to IT worker. \n" +
                           "11. Unregister worker. \n" +
                           "12. Exit. \n" +
                           "****************************************************************");

                case "rolManager":

                    return ("\n****************************************************************\n" +
                           "                          MANAGER MODE                            \n" +
                           "__________________________________________________________________\n" +
                           "1. List team members\n" +
                           "2. List unassigned tasks. \n" +
                           "3. List task assignments. \n" +
                           "4. Assign IT worker to a team as technician. \n" +
                           "5. Assign task to IT worker. \n" +
                           "6. Exit. \n" +
                           "****************************************************************");

                case "rolTech":

                    return ("\n****************************************************************\n" +
                          "                       TECHNICIAN MODE                              \n" +
                           "___________________________________________________________________\n" +
                          "1. List unassigned tasks. \n" +
                          "2. List tasks of the team. \n" +
                          "3. Assing task to me. \n" +
                          "4. Exit.                      \n" +
                          "****************************************************************");

                default:

                    return null;
            }
        }
    }
}
