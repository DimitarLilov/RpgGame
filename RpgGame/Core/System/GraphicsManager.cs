namespace RpgGame.Systems
{
    using RLNET;
    using RpgGame.Core;
    using RpgGame.Models;
    using RpgGame.Models.Map;
    using RpgGame.Utilities;

    public class GraphicsManager
    {
        private readonly TempDatabase db;
        public GraphicsManager(TempDatabase db)
        {
            this.db = db;
        }

        private DungeonMap Map => this.db.DungeonMap;

        private Player Player => this.db.Player;

        public void Draw(RLConsole mapConsole, RLConsole statConsole, RLConsole messageConsole)
        {
            this.Map.Draw(mapConsole, statConsole);
            this.Player.Draw(mapConsole, this.Map);
            this.Player.DrawStats(statConsole);
            MessageLog.Draw(messageConsole);
        }
    }
}