using System;
using System.ComponentModel.DataAnnotations;

namespace RpgGame.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
}