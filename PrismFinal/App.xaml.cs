using DryIoc;
using ModuleDialog;
using Prism.Ioc;
using Prism.Modularity;
using PrismFinal.Views;
using System.Windows;

namespace PrismFinal
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(Database.DatabaseModule));
            moduleCatalog.AddModule(typeof(ModuleUI.ModuleUIModule));
            moduleCatalog.AddModule(typeof(ModuleDialogModule));
        }
    }
}