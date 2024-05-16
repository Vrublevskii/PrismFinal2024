using Database.Contexts;
using Prism.Ioc;
using Prism.Modularity;
using DryIoc;
using Prism.DryIoc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Database.Utils;


namespace Database
{
    public class DatabaseModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterContext<EmployeeContext>();

            var container = containerRegistry.GetContainer();
            using var context = container.Resolve<EmployeeContext>();
        }
    }
}
