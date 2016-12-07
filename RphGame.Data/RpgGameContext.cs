using System.Net.Mail;
using RpgGame.Models;
using RpgGame.Models.Characters;

namespace RpgGame.Data
{
    using RpgGame.Models;

    using System.Data.Entity;

    public class RpgGameContext : DbContext
    {
        public RpgGameContext()
            : base("name=RpgGameContext")
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Mage>().ToTable("Mages");
            //modelBuilder.Entity<Warrior>().ToTable("Warriors");
            //modelBuilder.Entity<Paladin>().ToTable("Paladins");

            base.OnModelCreating(modelBuilder);
        }
    }
}