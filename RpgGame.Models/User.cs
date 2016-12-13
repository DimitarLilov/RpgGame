using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class User
    {
        public User()
        {
            this.Players = new HashSet<Player>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime RegisteredDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}