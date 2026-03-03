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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workers>().HasData(
                new Workers { Id = 1, Name = "Nagy Mátyás", WorkId = 1, Exp = 3 },
                new Workers { Id = 2, Name = "Kis Elek", WorkId = 2, Exp = 1 },
                new Workers { Id = 3, Name = "Nagy Milán", WorkId = 6, Exp = 5 },
                new Workers { Id = 4, Name = "Tamás András", WorkId = 3, Exp = 7 },
                new Workers { Id = 5, Name = "Póka Andrea", WorkId = 1, Exp = 2 }
                );

        }
    }
}
