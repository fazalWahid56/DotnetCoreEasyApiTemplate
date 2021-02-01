using App.Db.DTO;
using Microsoft.EntityFrameworkCore;

namespace App.Db
{
    public class AppContext:DbContext
    {
        public AppContext(DbContextOptions options) : base(options)
        { }

        public DbSet<EmployeeDto> Employees { get; set; }
    }
}
