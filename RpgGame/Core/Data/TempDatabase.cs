namespace RpgGame.Core
{
    using RpgGame.Models;
    using RpgGame.Models.Map;

    public class TempDatabase
    {
        public Player Player { get; set; }

        public DungeonMap DungeonMap { get; set; }
    }
}