using Database.Contexts;
using Database.Entity;
using Database.Utils;
using Microsoft.EntityFrameworkCore;

namespace Database.Managers.EntityManager
{
    /*
    Есть подозрение, что данный класс:
        1) будет использоваться раз в полгода или реже;
        2) будет использоватся единожды за один запуск приложения;
        3) класс не настолько большой, чтобы замедлить приложение, даже если будет вызываться несколько раз;
        4) класс вызывается по нажатию кнопки пользователем; нет тригеров, которые могли бы вызвать его несколько раз в секунду;
    поэтому обьект класса не сохраняется и создаётся по новому каждый раз при необходимости.
     */
    public class PositionManager : IDbEntityManager
    {
        private PositionManager() { }

        internal static IDbEntityManager GetInstance() => new PositionManager();
        public void GetEntitiesFromDbForContext(EmployeeContext employeeContext) => employeeContext.Positions.FromSqlRaw($"SELECT * FROM Positions");
        public async Task CreateInContext(EmployeeContext employeeContext, DbEntity position)  => await employeeContext.Positions.AddAsync((Position)position);
        public void UpdateInContext(DbEntity position, DbEntity emptyPosition) => position.ObjectClonerWithout(emptyPosition, "Employees");
        public void DeleteInContext(EmployeeContext employeeContext, DbEntity position) => employeeContext.Positions.Remove((Position)position);
        public DbEntity GetCloneEntityNoDependencies(DbEntity position) => new Position() { NamePosition = ((Position)position).NamePosition };
    }
}
