using System.Data.Entity;
using RpgGame.Models;

namespace RpgGame.Data.Data
{
    public class RpgGameContext : DbContext
    {
        public RpgGameContext()
            : base("name=RpgGameContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<RpgGameContext>());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Player> Players { get; set; }
        public IDbSet<Character> Characters { get; set; }
        public IDbSet<Monster> Monsters { get; set; }
        public IDbSet<DungeonMap> Dungeons { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<Door> Doors { get; set; }
        public IDbSet<Tunel> Tunels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tunel>()
                .HasRequired(t => t.Door)
                .WithRequiredDependent(d => d.Tunel);

            base.OnModelCreating(modelBuilder);
        }
    }
}