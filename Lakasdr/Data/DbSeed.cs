using Lakasdr.Models;

namespace Lakasdr.Data
{
    public static class DbSeed
    {
        public static void Seed(WorkDbContext seed)
        {
            if (seed.Ratings.Count()<=1)
            {
                seed.Ratings.Add(new Ertekeles
                {
                    Ertek = 5,
                    Desc = "Maximálisan elégedett vagyok! A kivitelezés gyors, precíz és igényes volt. A csapat végig profi módon állt a munkához, és minden részletre odafigyeltek. Ritka az ilyen megbízható szolgáltatás, csak ajánlani tudom!",
                    Email = "toth.mark0603@gmail.com",
                    Ideje = DateTime.Now
                });
                seed.Ratings.Add(new Ertekeles
                {
                    Ertek = 4,
                    Desc = "Nagyon jó tapasztalataim voltak a céggel. A munka minősége kifogástalan, és alapvetően tartották a megbeszélteket. Apróbb csúszások előfordultak, de összességében elégedett vagyok, szívesen ajánlom őket.",
                    Email = "peldaferi11@gmail.com",
                    Ideje = DateTime.Now
                });
                seed.Ratings.Add(new Ertekeles
                {
                    Ertek = 3,
                    Desc = "A munka elfogadható lett, de volt néhány kellemetlenség. A kivitelezés rendben volt, viszont a kommunikáció nem mindig működött jól, és a határidők csúsztak. Közepes élmény, van hova fejlődni.",
                    Email = "kiskovacskata01@gmail.com",
                    Ideje = DateTime.Now
                });
                seed.Ratings.Add(new Ertekeles
                {
                    Ertek = 2,
                    Desc = "Sajnos nem voltam teljesen elégedett. Több alkalommal is csúsztak a határidők, és a kivitelezés minősége sem mindenhol volt megfelelő. Pozitívum, hogy próbálták javítani a hibákat, de összességében többet vártam.",
                    Email = "durvamark200603@gmail.com",
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

            if (!seed.Images.Any())
            {
                seed.Images.AddRange(
                    new Image
                    {
                       
                        Nev = "terasz",
                        FilePath = "/images/kep1.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "kerités",
                        FilePath = "/images/kep2.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "színezés",
                        FilePath = "/images/kep3.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "fal építés",
                        FilePath = "/images/kep4.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "csempészés",
                        FilePath = "/images/kep5.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "kert",
                        FilePath = "/images/kep6.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "kert2",
                        FilePath = "/images/kep7.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "csempézés2",
                        FilePath = "/images/kep8.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "tető",
                        FilePath = "/images/kep9.jpg",
                        UploadDate = DateTime.Now,
                    },
                    new Image
                    {

                        Nev = "betonozás",
                        FilePath = "/images/kep10.jpg",
                        UploadDate = DateTime.Now,
                    }
                );

            }

            seed.SaveChanges();
        }
    }
}