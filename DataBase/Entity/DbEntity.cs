using Database.Managers.EntityManager;

namespace Database.Entity
{
    public abstract class DbEntity 
    {
        public abstract IDbEntityManager GetManager();
    }
}
