using Microsoft.EntityFrameworkCore;

namespace GBC_Travel_Group53.Models.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext>opts ):base(opts)
        {
            
        }
        public DbSet<Person> Person { get; set; }
    }
}
