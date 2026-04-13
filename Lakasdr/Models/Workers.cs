using System.ComponentModel.DataAnnotations;

namespace Lakasdr.Models
{
    public class Workers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A név megadása kötelező.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A munka kiválasztása kötelező.")]
        public int WorkId { get; set; }

        [Required(ErrorMessage = "A tapasztalat megadása kötelező.")]
        public int Exp { get; set; }

        public Jobs? Jobs { get; set; }
    }
}