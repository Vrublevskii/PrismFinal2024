using Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }

        public EmployeeContext(DbContextOptions<EmployeeContext> opt) : base(opt) => Database.EnsureCreated();        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasAlternateKey(d => d.NameDepartment);
            modelBuilder.Entity<Position>().HasAlternateKey(p => p.NamePosition);
        }
    }
}
