using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgGame.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [NotMapped]
        public int XCenter => this.X + this.Width/2;

        [NotMapped]
        public int YCenter => this.Y + this.Height / 2;

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public virtual DungeonMap DungeonMap { get; set; }
    }
}