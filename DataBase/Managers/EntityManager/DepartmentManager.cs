using Database.Contexts;
using Database.Entity;
using Database.Utils;
using Microsoft.EntityFrameworkCore;

namespace Database.Managers.EntityManager
{
    /*
    Есть подозрение, что данный класс:
        1) будет использоваться раз в полгода или реже;
        2) будет использоватся единожды за один запуск приложения (создать подразделение утром, а потом ещё одно через пару часов - ход гения);
    поэтому обьект класса не сохраняется (экономия памяти) и создаётся по новому каждый раз при необходимости.
     */
    public class DepartmentManager : IDbEntityManager
    {
        private DepartmentManager() { }
        internal static IDbEntityManager GetInstance() => new DepartmentManager();
        public void GetEntitiesFromDbForContext(EmployeeContext employeeContext) => employeeContext.Departments.FromSqlRaw($"SELECT * FROM Departments");
        public async Task CreateInContext(EmployeeContext employeeContext, DbEntity department) => await (employeeContext).Departments.AddAsync((Department)department);
        public void UpdateInContext(DbEntity department, DbEntity emptyDbEntity) => department.ObjectClonerWithout(emptyDbEntity, "Employees");
        public void DeleteInContext(EmployeeContext employeeContext, DbEntity department) => employeeContext.Departments.Remove((Department)department);
        public DbEntity GetEmptyForDialog() => new Department();
        public DbEntity GetCloneEntityNoDependencies(DbEntity department) => new Department() { NameDepartment = ((Department)department).NameDepartment };
    }
}
