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
        public DbSet<Ertekeles>Ratings { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workers>().HasData(
                new Workers { Id = 1, Name = "Nagy Mátyás", WorkId = 1, Exp = 3 },
                new Workers { Id = 2, Name = "Kis Elek", WorkId = 2, Exp = 1 },
                new Workers { Id = 3, Name = "Nagy Milán", WorkId = 4, Exp = 5 },
                new Workers { Id = 4, Name = "Tamás András", WorkId = 3, Exp = 7 },
                new Workers { Id = 5, Name = "Póka Andrea", WorkId = 1, Exp = 2 }
                );
            modelBuilder.Entity<Jobs>().HasData(
                 new Jobs
                 {
                     Id = 1,
                     Name = "Fürdőszoba felújítás",
                     Description = "A fürdőszoba felújítása során modern burkolatok, " +
                 "új szaniterek és energiatakarékos világítás került beépítésre, így a helyiség letisztult és időtálló megjelenést kapott."
                 },
                 new Jobs
                 {
                     Id = 2,
                     Name = "Hálószoba felújítás",
                     Description = "A hálószoba átalakítása friss falfestéssel, meleg hatású" +
                 " padlóburkolattal és egyedi beépített szekrénnyel történt, amely nyugodt, harmonikus légkört teremt."
                 },
                 new Jobs
                 {
                     Id = 3,
                     Name = "Konyha felújítás",
                     Description = "A konyha felújítása során korszerű konyhabútor, praktikus " +
                 "tárolási megoldások és modern gépek kerültek beépítésre, hogy a főzés kényelmesebb és hatékonyabb legyen."
                 },
                 new Jobs
                 {
                     Id = 4,
                     Name = "Kocsi beálló térkövezés",
                     Description = "A kocsi bejáró térkövezése strapabíró, esztétikus " +
                 "térkövekkel készült, biztosítva a tartós, stabil burkolatot és az igényes megjelenést."
                 }
                 );

        }
    }
}
