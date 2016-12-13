using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Door
    {
        [Key]
        public int Id { get; set; }

        public bool IsOpen { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public virtual Tunel Tunel { get; set; }
    }
}