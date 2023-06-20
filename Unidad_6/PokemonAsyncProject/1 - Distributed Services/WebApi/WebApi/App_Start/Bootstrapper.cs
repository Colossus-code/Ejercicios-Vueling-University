using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            //Configure AutoFac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
        }

    }
}