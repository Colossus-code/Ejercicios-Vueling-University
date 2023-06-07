using Autofac;
using Bussines.IService;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = CreateContainer();
            var mainMenu = new Menu(
                container.Resolve<ILoginVerification>(),
                container.Resolve<IMenuManager>());

            mainMenu.StartProgram();
        }

        private static IContainer CreateContainer()
        {
            var registrator = new ReflectionRegistrator();
            var container = registrator.RegisterDependencies();

            return container; 
        }
    }
}
