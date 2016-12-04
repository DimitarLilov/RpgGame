namespace RpgGame.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RogueSharp;
    using RogueSharp.DiceNotation;
    using RpgGame.Behaviors;
    using RpgGame.Models;
    using RpgGame.Models.Map;
    using RpgGame.Models.Monsters;

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
                this.CreateDoors(room);
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
                player = new Player("Guest");
            }

            player.X = this.map.Rooms[0].DungeonRoom.Center.X;
            player.Y = this.map.Rooms[0].DungeonRoom.Center.Y;

            this.map.AddPlayer(player);
        }

        private void CreateDoors(Room room)
        {
            int xMin = room.DungeonRoom.Left;
            int xMax = room.DungeonRoom.Right;
            int yMin = room.DungeonRoom.Top;
            int yMax = room.DungeonRoom.Bottom;

            List<Cell> borderCells = this.map.GetCellsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange(this.map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange(this.map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange(this.map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            foreach (Cell cell in borderCells)
            {
                if (this.IsPotentialDoor(cell))
                {
                    this.map.SetCellProperties(cell.X, cell.Y, false, true);
                    this.map.Doors.Add(new Door
                    {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                }
            }
        }

        private bool IsPotentialDoor(Cell cell)
        {
            if (!cell.IsWalkable)
            {
                return false;
            }

            Cell right = this.map.GetCell(cell.X + 1, cell.Y);
            Cell left = this.map.GetCell(cell.X - 1, cell.Y);
            Cell top = this.map.GetCell(cell.X, cell.Y - 1);
            Cell bottom = this.map.GetCell(cell.X, cell.Y + 1);

            if (this.map.GetDoor(cell.X, cell.Y) != null ||
                 this.map.GetDoor(right.X, right.Y) != null ||
                 this.map.GetDoor(left.X, left.Y) != null ||
                 this.map.GetDoor(top.X, top.Y) != null ||
                 this.map.GetDoor(bottom.X, bottom.Y) != null)
            {
                return false;
            }

            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)
            {
                return true;
            }

            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)
            {
                return true;
            }

            return false;
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
                            var monster = new Orc(null);
                            monster.X = randomRoomLocation.X;
                            monster.Y = randomRoomLocation.Y;
                            monster.Behavior = new StandardMoveAndAttack();
                            this.map.AddMonster(monster);
                        }
                    }
                }
            }
        }
    }
}