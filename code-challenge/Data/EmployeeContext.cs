using challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace challenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        //Task 2 - Create a dbset for Compensations
        public DbSet<Compensation> Compensation { get; set; }
    }
}
