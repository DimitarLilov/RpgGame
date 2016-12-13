using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Tunel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int XStart { get; set; }

        [Required]
        public int YStart { get; set; }

        [Required]
        public int XEnd { get; set; }

        [Required]
        public int YEnd { get; set; }

        public virtual Door Door { get; set; }

        [Required]
        public virtual DungeonMap DungeonMap { get; set; }
    }
}