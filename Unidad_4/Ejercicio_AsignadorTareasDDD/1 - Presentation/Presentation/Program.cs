using Autofac;
using Bussines.IServices;
using Presentation.Contracts;
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
            var mainMenu = new MainMenu(
                container.Resolve<ILogin>(),
                container.Resolve<ILoginMenu>(),
                container.Resolve<IPrinterMenuOptions>(),
                container.Resolve<IMenuManage>());

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
