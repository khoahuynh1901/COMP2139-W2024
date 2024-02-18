using Microsoft.EntityFrameworkCore;
using Week04.Models.Entities;

namespace Week04.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Courses> Courses { get; set; }

    }
}
