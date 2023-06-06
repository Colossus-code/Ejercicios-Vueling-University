﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.IServices
{
    public interface IPrinterServiceAdmin
    {
        string printerRepositoryTeamNames();
        string printerRepositoryITWorkersByTeamNames(string teamName);
        string printerRepositoryUnassignedTask();
        string printerRepositoryTaskByTeamName(string teamName);

    }
}
