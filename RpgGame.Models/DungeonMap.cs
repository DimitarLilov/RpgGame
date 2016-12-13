using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgGame.Models
{
    [Table("Dungeons")]
    public class DungeonMap
    {
        public DungeonMap()
        {
            this.Rooms = new HashSet<Room>();
            this.Monsters = new HashSet<Monster>();
            this.Tunels = new HashSet<Tunel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Level { get; set; }

        public ICollection<Room> Rooms { get; set; }

        public ICollection<Monster> Monsters { get; set; }

        public ICollection<Tunel> Tunels { get; set; }
    }
}