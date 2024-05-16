using Database.Managers.EntityManager;

namespace Database.Entity
{
    public partial class Position : DbEntity
    {
        public int Id { get; set; }
        public string NamePosition { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();            
        public override IDbEntityManager GetManager() => PositionManager.GetInstance();
    }
}