using Lakasdr.Models;

namespace Lakasdr.Data
{
    public static class DbSeed
    {
        public static void Seed(WorkDbContext seed)
        {
            if (!seed.Ratings.Any())
            {
                seed.Ratings.Add(new Ertekeles
                {
                    Ertek = 4,
                    Desc = "nagyon jó!",
                    Email = "toth.mark0603@gmail.com",
                    Ideje = DateTime.Now
                });
            }

            if (!seed.Jobs.Any())
            {
                seed.Jobs.AddRange(
                    new Jobs
                    {
                        Name = "Fürdőszoba felújítás",
                        Description = "A fürdőszoba felújítása során modern burkolatok, új szaniterek és energiatakarékos világítás került beépítésre, így a helyiség letisztult és időtálló megjelenést kapott."
                    },
                    new Jobs
                    {
                        Name = "Hálószoba felújítás",
                        Description = "A hálószoba átalakítása friss falfestéssel, meleg hatású padlóburkolattal és egyedi beépített szekrénnyel történt, amely nyugodt, harmonikus légkört teremt."
                    },
                    new Jobs
                    {
                        Name = "Konyha felújítás",
                        Description = "A konyha felújítása során korszerű konyhabútor, praktikus tárolási megoldások és modern gépek kerültek beépítésre, hogy a főzés kényelmesebb és hatékonyabb legyen."
                    },
                    new Jobs
                    {
                        Name = "Kocsi beálló térkövezés",
                        Description = "A kocsi bejáró térkövezése strapabíró, esztétikus térkövekkel készült, biztosítva a tartós, stabil burkolatot és az igényes megjelenést."
                    }
                );

                seed.SaveChanges();
            }

            if (!seed.Workers.Any())
            {
                seed.Workers.AddRange(
                    new Workers { Name = "Nagy Mátyás", WorkId = 1, Exp = 3 },
                    new Workers { Name = "Kis Elek", WorkId = 2, Exp = 1 },
                    new Workers { Name = "Nagy Milán", WorkId = 4, Exp = 5 },
                    new Workers { Name = "Tamás András", WorkId = 3, Exp = 7 },
                    new Workers { Name = "Póka Andrea", WorkId = 1, Exp = 2 }
                );
            }

            seed.SaveChanges();
        }
    }
}