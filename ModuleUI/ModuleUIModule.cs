using DataTransferService.Enums;
using ModuleUI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleUI
{
    public class ModuleUIModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.ContentRegion, typeof(ResultListUserControl));
            regionManager.RegisterViewWithRegion(Regions.EmployeeResultRegion, typeof(EmployeeList));
            regionManager.RegisterViewWithRegion(Regions.DepartmentResultRegion, typeof(DepartmentList));
            regionManager.RegisterViewWithRegion(Regions.PositionResultRegion, typeof(PositionList));
            regionManager.RegisterViewWithRegion(Regions.ControlsRegion, typeof(ControlsUserControl));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
