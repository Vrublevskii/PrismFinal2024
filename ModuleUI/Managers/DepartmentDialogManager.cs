using Database.Contexts;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Prism.Services.Dialogs;
using System.Windows;
using static DataTransferService.Enums.ParamStrings;
using static DataTransferService.Enums.ViewModelsNamesStrings;
using static ModuleUI.Managers.Utils<Database.Entity.Department>;

namespace ModuleUI.Managers
{
    public class DepartmentDialogManager : IDialogManager
    {
        EmployeeContext _employeeContext;
        IDialogService _dialogService;

        public DepartmentDialogManager(EmployeeContext employeeContext, IDialogService dialogService)
        {
            _employeeContext = employeeContext;
            _dialogService = dialogService;
        }

        public void ShowAddDialog(DbEntity departmentToAdd, List<Employee>? employees, List<Department>? departments, List<Position>? positions)
        {
            DialogParameters parameters = new DialogParameters
            {
                {TitleString, TitleAddDepartmentValueString },
                {EntityString, departmentToAdd },
                {DepartmentsString, positions },
                {OkButtonContentString, OkAddDepartmentValueString }
            };
            AddDialogIfErrorRepeat(parameters, departmentToAdd, departments);
        }

        private void AddDialogIfErrorRepeat(DialogParameters parameters, DbEntity positionsToAdd, List<Department>? departments)
        {
            _dialogService.ShowDialog(AddEditDepartmentString, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                Department positionsToAddAsPosition = (Department)positionsToAdd;
                string namePosition = positionsToAddAsPosition.NameDepartment;
                bool positionAlreadyPresent = false;
                foreach (var dep in departments)
                {
                    if (dep.NameDepartment.Equals(namePosition))
                    {
                        MessageBox.Show("Такой отдел уже есть");
                        AddDialogIfErrorRepeat(parameters, positionsToAddAsPosition, departments);
                        positionAlreadyPresent = true;
                        _employeeContext.Positions.FromSqlRaw($"SELECT * FROM Departments");
                        break;
                    }
                }
                await CreateEntityInContextIfFalse(positionAlreadyPresent, _employeeContext, "Такой отдел уже есть", parameters, positionsToAddAsPosition, departments, AddDialogIfErrorRepeat);
            });
        }

        /*
        public void ShowAddDialog(IDialogService dialogService,
            BindableBase invokerViewModel,
            DbEntity emptyEntity,
            Action<IDialogResult> callBack)
        {
            ControlsViewModel viewModel = (ControlsViewModel)invokerViewModel;
            DialogParameters parameters = new DialogParameters
            {
                {ParamStrings.Title, ParamStrings.TitleAddDepartmentValue },
                {ParamStrings.Entity, emptyEntity },
                {ParamStrings.OkButtonContent, ParamStrings.OkAddDepartmentValue }
            };
            dialogService.ShowDialog(ViewModelsNamesStrings.AddEditDepartment, parameters, callBack);
        }

        public void ShowEditDialog(IDialogService dialogService,
            DbEntity entityFromContext,
            DbEntity entityEmployeeToChange,
            BindableBase invokerViewModel,
            Action<IDialogResult> callBack)
        {
            ControlsViewModel viewModel = (ControlsViewModel)invokerViewModel;
            Department departmentFromContext = (Department)entityFromContext;
            Department empltyDepartmentToChange = (Department)entityEmployeeToChange;

            empltyDepartmentToChange.ObjectClonerWithout(departmentFromContext, ClonerFields.Employees);

            var parameters = new DialogParameters
            {
                {ParamStrings.Title, ParamStrings.TitleUpdateEmployeeValue},
                {ParamStrings.Entity, empltyDepartmentToChange},
                {ParamStrings.OkButtonContent, ParamStrings.OkUpdateDepartmentValue }
            };
            dialogService.ShowDialog(ViewModelsNamesStrings.AddEditDepartment, parameters, callBack);
        }

        public void CallBackCreateInContextAndSave(ControlsViewModel controlsViewModel,
            DbEntity entity)
        {
            throw new NotImplementedException();
        }
        */

        public DbEntity GetEmptyEntity() => new Department();
        public void ShowEditDialog(DbEntity entityEmployeeToChange, List<Department>? departments, List<Position>? positions)
        {
            throw new NotImplementedException();
        }
    }
}