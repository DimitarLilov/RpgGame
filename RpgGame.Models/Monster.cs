using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgGame.Models
{
    [Table("Monsters")]
    public class Monster : Character
    {
        public int? TurnsAlerted { get; set; }

        [Required]
        public virtual DungeonMap DungeonMap { get; set; }
    }
}