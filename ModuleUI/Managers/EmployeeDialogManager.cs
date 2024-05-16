using Database.Contexts;
using Database.Entity;
using Database.Managers.EntityManager;
using Microsoft.EntityFrameworkCore;
using Prism.Services.Dialogs;
using System.Windows;
using static DataTransferService.Enums.ParamStrings;
using static DataTransferService.Enums.ViewModelsNamesStrings;

namespace ModuleUI.Managers
{
    /*
    Находится в UI, потому что вызывается из UI.
    Проверять мыло на уникальность в самом приложении перед отправкой запроса в ДБ не стал, т.к. если записей много (на Белазе работает порядка 8,5 тысяч человек) наверное оно того не стоит.
    */
    public class EmployeeDialogManager : IDialogManager
    {
        #region Field
        private EmployeeContext _employeeContext;
        private IDialogService _dialogService;
        #endregion

        public EmployeeDialogManager(EmployeeContext employeeContext, IDialogService dialogService)
        {
            _employeeContext = employeeContext;
            _dialogService = dialogService;
        }

        #region Methods
        public void ShowAddDialog(DbEntity emptyEmployeeToAdd, List<Employee> employees, List<Department> departments, List<Position> positions)
        {
            DialogParameters parameters = new DialogParameters
            {
                {TitleString, TitleAddEmployeeValueString},
                {EntityString, emptyEmployeeToAdd },
                {DepartmentsString, departments},
                {PositionsString, positions},
                {OkButtonContentString, OkAddEmployeeValueString}
            };
            AddDialogIfErrorRepeat(parameters, emptyEmployeeToAdd, employees);
        }

        private void AddDialogIfErrorRepeat(DialogParameters parameters, DbEntity employeeToAdd, List<Employee> employees)
        {
            _dialogService.ShowDialog(AddEditEmployeeString, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                await _employeeContext.Employees.AddAsync((Employee)employeeToAdd);
                try
                {
                    await _employeeContext.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Такое мыло уже есть");
                    employeeToAdd.GetManager().GetEntitiesFromDbForContext(_employeeContext);
                    AddDialogIfErrorRepeat(parameters, employeeToAdd, employees);
                }
            });
        }

        public void ShowEditDialog(DbEntity employeeToChange, List<Department>? departments, List<Position>? positions)
        {
            DbEntity employeeForDialog = GetCloneForEmployee(employeeToChange, departments, positions);
            var parameters = new DialogParameters
            {
                {TitleString, TitleAddEmployeeValueString},
                {EntityString, employeeForDialog },
                {DepartmentsString, departments},
                {PositionsString, positions},
                {OkButtonContentString, OkAddEmployeeValueString}
            };
            EditDialogRecursionPart(parameters, employeeToChange, employeeForDialog);
        }

        private void EditDialogRecursionPart(DialogParameters parameters, DbEntity employeeToChange, DbEntity employeeForDialog)
        {
            /*
            _dialogService.ShowDialog(AddEditEmployeeString, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                //DbEntity casheEmployee = CloneEmployeeFull(employeeToChange);
                entityManager.UpdateInContext(employeeToChange, employeeForDialog);
                try
                {
                    await _employeeContext.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Такое мыло уже есть");
                    entityManager.UpdateInContext(employeeToChange, casheEmployee);
                    EditDialogRecursionPart(parameters, employeeToChange, employeeForDialog);
                }
            });*/
        }
        #endregion
        /*
        private Employee GetCloneForEmployee(DbEntity employeeToCopy)
        {
            Employee employee = new();
            Employee originalEmployee = (Employee)employeeToCopy;
            employee.LastName = originalEmployee.LastName;
            employee.Name = originalEmployee.Name;
            employee.Patronymic = originalEmployee.Patronymic;
            employee.DateOfBirth = originalEmployee.DateOfBirth;
            employee.Telephone = originalEmployee.Telephone;
            employee.Email = originalEmployee.Email;
            employee.Position = originalEmployee.Position;
            employee.Department = originalEmployee.Department;



            employee.ObjectClonerWithout(employeeToCopy,
                ClonerFields.Department,
                ClonerFields.Position);
            employee.Department = _departments.Find(d =>
                d.NameDepartment.Equals(employeeToCopy.Department.NameDepartment));
            employee.Position = _positions.Find(d =>
                d.NamePosition.Equals(employeeToCopy.Position.NamePosition));
            return employee;
            return entityToCopy;
        }*/
        //Коллекции не могут быть null.
        private Employee GetCloneForEmployee(DbEntity employeeToCopy, List<Department> departments, List<Position> positions)
        {
            Employee originalEmployee = (Employee)employeeToCopy;
            return new Employee()
            {
                LastName = originalEmployee.LastName,
                Name = originalEmployee.Name,
                Patronymic = originalEmployee.Patronymic,
                DateOfBirth = originalEmployee.DateOfBirth,
                Telephone = originalEmployee.Telephone,
                Email = originalEmployee.Email,
                Department = departments.Find(d => d.NameDepartment.Equals(originalEmployee.Department.NameDepartment)),
                Position = positions.Find(d => d.NamePosition.Equals(originalEmployee.Position.NamePosition))
            };
        }

        public DbEntity GetEmptyEntity() => new Employee();
    }
}
