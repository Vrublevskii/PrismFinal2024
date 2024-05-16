using Database.Contexts;
using Database.Entity;

namespace Database.Managers.EntityManager
{
    public interface IDbEntityManager
    {
        public Task CreateInContext(EmployeeContext employeeContext, DbEntity dbEntityToAdd);
        public void GetEntitiesFromDbForContext(EmployeeContext employeeContext);
        public void UpdateInContext(DbEntity dbEntity, DbEntity updatedDbEntity);
        public void DeleteInContext(EmployeeContext employeeContext, DbEntity dbEntityToDelete);
        public DbEntity GetCloneEntityNoDependencies(DbEntity entity);
    }
}