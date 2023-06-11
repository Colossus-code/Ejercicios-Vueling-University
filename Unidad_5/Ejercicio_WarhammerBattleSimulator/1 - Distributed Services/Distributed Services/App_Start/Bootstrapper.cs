using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Distributed_Services
{
    using Autofac.Core.Activators.Reflection;
    using Presentation.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    namespace AutoFacWithWebAPI.App_Start
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
}