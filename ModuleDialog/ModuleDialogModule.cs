using Prism.Ioc;
using Prism.Modularity;
using PrismFinal.ModuleDialog.Views;
using TestProjectPrism.DialogModule.ViewModels;
using TestProjectPrism.DialogModule.Views;
using TestProjectPrism.ViewModels;

namespace ModuleDialog
{
    public class ModuleDialogModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AddEditEmployee, AddEditEmployeeViewModel>();
            containerRegistry.RegisterDialog<AddEditDepartment, AddEditDepartmentViewModel>();
            containerRegistry.RegisterDialog<AddEditPosition, AddEditPositionViewModel>();
        }
    }
}
