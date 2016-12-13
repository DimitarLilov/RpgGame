using RpgGame.Models;

namespace RpgGame.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Player> Players { get; }
        IRepository<Monster> Monsters { get; }
        IRepository<DungeonMap> Dungeons { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Door> Doors { get; }
        IRepository<Tunel> Tunels { get; }

        void Commit();
    }
}