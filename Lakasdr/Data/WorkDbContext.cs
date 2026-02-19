using Lakasdr.Models;
using Microsoft.EntityFrameworkCore;

namespace Lakasdr.Data
{
    public class WorkDbContext : DbContext
    {
        public WorkDbContext(DbContextOptions<WorkDbContext> options):base(options)
        {
            
        }

       public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Work> Work { get; set; }
        public DbSet<Workers> Workers { get; set; }
        public DbSet<Image>Images { get; set; }
    }
}
