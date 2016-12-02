namespace RpgGame.Core
{
    using RogueSharp;

    public class Room
    {
        private Rectangle dungeonRoom;

        public Room(int x, int y, int width, int height)
        {
            this.dungeonRoom = new Rectangle(x, y, width, height);
        }

        public Rectangle DungeonRoom
        {
            get { return this.dungeonRoom; }
            set { this.dungeonRoom = value; }
        }
    }
}
