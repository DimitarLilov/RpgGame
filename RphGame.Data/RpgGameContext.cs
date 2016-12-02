namespace RpgGame.Data
{
    using RpgGame.Data.Models;

    using System.Data.Entity;

    public class RpgGameContext : DbContext
    {
        public RpgGameContext()
            : base("name=RpgGameContext")
        {
        }

        public IDbSet<User> Users { get; set; }
    }
}