namespace Lakasdr.Models
{
    public class Ertekeles
    {
        public int Id { get; set; }
        public int Ertek {  get; set; }
        public DateTime Ideje { get; set; } 

        public string Desc { get; set; }

        List<Ertekeles> Ertekelesek = new List<Ertekeles>(); // Db-be berakni majd

    }

    

}
