namespace RpgGame.Models.Map
{
    using RogueSharp;

    public class Room
    {
        private readonly Rectangle dungeonRoom;

        public Room(int x, int y, int width, int height)
        {
            this.dungeonRoom = new Rectangle(x, y, width, height);
        }

        public Rectangle DungeonRoom => this.dungeonRoom;
    }
}
