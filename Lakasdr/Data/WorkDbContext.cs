using Lakasdr.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        public DbSet<Ertekeles>Ratings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            
        }



            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
        }


    }
    
}
