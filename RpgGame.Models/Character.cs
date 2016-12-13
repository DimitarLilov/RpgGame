using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RpgGame.Models
{
    [Table("Characters")]
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Awareness { get; set; }

        public int Steps { get; set; }

        [Required]
        public int MinAttack { get; set; }

        [Required]
        public int MaxAttack { get; set; }

        [Required]
        public int MinDefence { get; set; }

        [Required]
        public int MaxDefence { get; set; }

        [Required]
        public int Gold { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int MaxHealth { get; set; }

        [Required]
        public int Speed { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }
    }
}