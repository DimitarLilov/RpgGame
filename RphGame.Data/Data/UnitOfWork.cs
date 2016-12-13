using RpgGame.Data.Interfaces;
using RpgGame.Models;

namespace RpgGame.Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RpgGameContext context;

        private IRepository<User> users;
        private IRepository<Player> players;
        private IRepository<Monster> monsters;
        private IRepository<DungeonMap> dungeons;
        private IRepository<Room> rooms;
        private IRepository<Door> doors;
        private IRepository<Tunel> tunels;

        public UnitOfWork(RpgGameContext context)
        {
            this.context = context;
        }

        public IRepository<User> Users => this.users ?? (this.users = new Repository<User>(this.context));
        public IRepository<Player> Players => this.players ?? (this.players = new Repository<Player>(this.context));
        public IRepository<Monster> Monsters => this.monsters ?? (this.monsters = new Repository<Monster>(this.context));
        public IRepository<DungeonMap> Dungeons => this.dungeons ?? (this.dungeons = new Repository<DungeonMap>(this.context));
        public IRepository<Room> Rooms => this.rooms ?? (this.rooms = new Repository<Room>(this.context));
        public IRepository<Door> Doors => this.doors ?? (this.doors = new Repository<Door>(this.context));
        public IRepository<Tunel> Tunels => this.tunels ?? (this.tunels = new Repository<Tunel>(this.context));

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}