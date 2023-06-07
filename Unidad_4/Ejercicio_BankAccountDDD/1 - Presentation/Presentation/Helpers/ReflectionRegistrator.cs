using Autofac;
using Bussines;
using Bussines.IService;
using Infrastructure.IRepository;
using Presentation.Contracts;
using Repository;
using Repository.DataBaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public class ReflectionRegistrator
    {
        public IContainer RegisterDependencies()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterCustomDependencies(containerBuilder);
            var container = containerBuilder.Build();
            return container;
        }

        private void RegisterCustomDependencies(ContainerBuilder container)
        {
            container.RegisterType<LoginVerification>().As<ILoginVerification>(); 
            container.RegisterType<BankAccountRepository>().As<IRepositoryBankAccount>(); 
            container.RegisterType<MovementsRepository>().As<IRepositoryMovements>(); 
            container.RegisterType<MenuManager>().As<IMenuManager>();
      

        }
    }
}
