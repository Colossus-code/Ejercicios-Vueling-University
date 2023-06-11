using Autofac;
using Distributed_Services.AutoFacWithWebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Distributed_Services
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            Bootstrapper.Run();
        }

    }
}
