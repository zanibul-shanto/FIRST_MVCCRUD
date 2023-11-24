using Microsoft.EntityFrameworkCore;
using MVCCRUD.Models.DomainModels;

namespace MVCCRUD.Data
{
    public class CrudDBContext : DbContext
    {
        public CrudDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
