using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProject.DataAccessLayer;

namespace TestProjectSavchenko.Infrastructure.Installers
{
    public class WizardInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            {
                container.Register(Component.For<DataFacade>().LifeStyle.Singleton);
                container.Register(Component.For<MapperProvider>().LifeStyle.Singleton);
            }
        }
    }
}