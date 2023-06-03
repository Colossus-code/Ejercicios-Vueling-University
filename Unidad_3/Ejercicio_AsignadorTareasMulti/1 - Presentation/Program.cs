using Autofac;
using Ejercicio_AsignadorTareasMulti._1___Presentation;
using Ejercicio_AsignadorTareasMulti._1___Presentation.Helpers;
using Ejercicio_AsignadorTareasMulti._2___Bussines.IServices;
using Ejercicio_AsignadorTareasMulti._4___Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_AsignadorTareasMulti
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

