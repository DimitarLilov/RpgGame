namespace RpgGame.Systems
{
    using System;
    using Core;
    using Core.Monsters;
    using RogueSharp;
    using RogueSharp.DiceNotation;

    public class MapGenerator
    {
        private readonly int width;
        private readonly int height;

        private readonly DungeonMap map;

        public MapGenerator(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            this.map.Initialize(this.width, this.height);

            var newRoom = new Room(1, 10, 10, 10);
            var new2Room = new Room(20, 1, 15, 10);
            var new3Room = new Room(35, 30, 10, 10);
            var new4Room = new Room(55, 30, 10, 10);

            this.map.Rooms.Add(newRoom);
            this.map.Rooms.Add(new2Room);
            this.map.Rooms.Add(new3Room);
            this.map.Rooms.Add(new4Room);

            for (int r = 0; r < this.map.Rooms.Count; r++)
            {
                if (r == 0)
                {
                    continue;
                }

                int previousRoomCenterX = this.map.Rooms[r - 1].DungeonRoom.Center.X;
                int previousRoomCenterY = this.map.Rooms[r - 1].DungeonRoom.Center.Y;
                int currentRoomCenterX = this.map.Rooms[r].DungeonRoom.Center.X;
                int currentRoomCenterY = this.map.Rooms[r].DungeonRoom.Center.Y;

                this.CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                this.CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);

                this.CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                this.CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
            }

            foreach (Room room in this.map.Rooms)
            {
                this.CreateRoom(room);
            }

            this.PlacePlayer();
            this.PlaceMonsters();
            return this.map;
        }

        private void PlacePlayer()
        {
            Player player = Engine.Player;
            if (player == null)
            {
                player = new Player();
            }

            player.X = this.map.Rooms[0].DungeonRoom.Center.X;
            player.Y = this.map.Rooms[0].DungeonRoom.Center.Y;

            this.map.AddPlayer(player);
        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x < Math.Max(xStart, xEnd); x++)
            {
                this.map.SetCellProperties(x, yPosition, true, true);
            }
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y < Math.Max(yStart, yEnd); y++)
            {
                this.map.SetCellProperties(xPosition, y, true, true);
            }
        }

        private void CreateRoom(Room room)
        {
            for (int x = room.DungeonRoom.Left + 1; x < room.DungeonRoom.Right; x++)
            {
                for (int y = room.DungeonRoom.Top + 1; y < room.DungeonRoom.Bottom; y++)
                {
                    this.map.SetCellProperties(x, y, true, true);
                }
            }
        }

        private void PlaceMonsters()
        {
            foreach (var room in this.map.Rooms)
            {
                if (Dice.Roll("1D10") < 7)
                {
                    var numberOfMonsters = Dice.Roll("1D4");
                    for (int i = 0; i < numberOfMonsters; i++)
                    {
                        Point randomRoomLocation = this.map.GetRandomWalkableLocationInRoom(room);

                        if (randomRoomLocation != null)
                        {
                            var monster = Orc.Create(1);
                            monster.X = randomRoomLocation.X;
                            monster.Y = randomRoomLocation.Y;
                            this.map.AddMonster(monster);
                        }
                    }
                }
            }
        }
    }
}