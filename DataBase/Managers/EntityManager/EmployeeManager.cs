using Database.Contexts;
using Database.Entity;
using Database.Utils;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Database.Managers.EntityManager
{
    /*
    Будет использоваться постоянно, поэтому - синглтон.
    При первом вызове создаётся обьект класса который сохраняется в память и не выгружается из неё до закрытия приложения.
     */
    public class EmployeeManager : IDbEntityManager
    {
        private static IDbEntityManager? _employeeManager;
        private EmployeeManager() { }
        internal static IDbEntityManager GetInstance() => _employeeManager ??= new EmployeeManager();
        public void GetEntitiesFromDbForContext(EmployeeContext employeeContext) => employeeContext.Employees.FromSqlRaw($"SELECT * FROM Employees");
        public async Task CreateInContext(EmployeeContext employeeContext, DbEntity dbEntityToAdd) => await (employeeContext).Employees.AddAsync((Employee)dbEntityToAdd);
        public void UpdateInContext(DbEntity selectedEmployee, DbEntity updatedEmployee) => CloneEmployee(selectedEmployee, updatedEmployee);
        /*
        {
            selectedEmployee.ObjectClonerWithout(updatedEmployee, "Department", "Position");
            selectedEmployee.Position = updatedEmployee.Position;
            selectedEmployee.Department = updatedEmployee.Department;
        }
        */
        public void DeleteInContext(EmployeeContext employeeContext, DbEntity employee) => employeeContext.Employees.Remove((Employee)employee);
        private void CloneEmployee(DbEntity employee, DbEntity employeeToClone)
        {

            Employee employeeAsEmployee = (Employee)employee;
            Employee employeeToCloneAsEmployee = (Employee)employeeToClone;

            employeeAsEmployee.LastName = employeeToCloneAsEmployee.LastName;
            employeeAsEmployee.Name = employeeToCloneAsEmployee.Name;
            employeeAsEmployee.Patronymic = employeeToCloneAsEmployee.Patronymic;
            employeeAsEmployee.DateOfBirth = employeeToCloneAsEmployee.DateOfBirth;
            employeeAsEmployee.Telephone = employeeToCloneAsEmployee.Telephone;
            employeeAsEmployee.Email = employeeToCloneAsEmployee.Email;
            employeeAsEmployee.Position = employeeToCloneAsEmployee.Position;
            employeeAsEmployee.Department = employeeToCloneAsEmployee.Department;
        }    

        public DbEntity GetCloneEntityNoDependencies(DbEntity employeeToClone)
        {
            Employee employee = (Employee)employeeToClone;
            return new Employee()
            {
                LastName = employee.LastName,
                Name = employee.Name,
                Patronymic = employee.Patronymic,
                DateOfBirth = employee.DateOfBirth,
                Telephone = employee.Telephone,
                Email = employee.Email
            };
        }
    }
}
