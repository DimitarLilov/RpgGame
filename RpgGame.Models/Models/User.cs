namespace RpgGame.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

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