namespace RpgGame.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using RLNET;
    using RogueSharp;

    public class DungeonMap : Map
    {
        private readonly List<Monster> monsters;
        private readonly List<TreasurePile> treasurePiles;
        private List<Room> rooms;
        private List<Door> doors;

        public DungeonMap()
        {
            this.monsters = new List<Monster>();
            this.rooms = new List<Room>();
            this.treasurePiles = new List<TreasurePile>();
            this.doors = new List<Door>();
        }

        public List<Room> Rooms
        {
            get { return this.rooms; }
        }

        public List<Door> Doors
        {
            get { return this.doors; }
            set { this.doors = value; }
        }

        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in this.GetAllCells())
            {
                this.SetConsoleSymbolForCell(mapConsole, cell);
            }

            int i = 0;

            foreach (Monster monster in this.monsters)
            {
                monster.Draw(mapConsole, this);

                if (this.IsInFov(monster.X, monster.Y))
                {
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }

            foreach (TreasurePile treasurePile in this.treasurePiles)
            {
                IDrawable drawableTreasure = treasurePile.Treasure as IDrawable;
                drawableTreasure?.Draw(mapConsole, this);
            }

            foreach (Door door in this.Doors)
            {
                door.Draw(mapConsole, this);
            }
        }

        public void UpdatePlayerFieldOfView()
        {
            Player player = Engine.Player;

            this.ComputeFov(player.X, player.Y, player.Awareness, true);

            foreach (Cell cell in this.GetAllCells())
            {
                if (this.IsInFov(cell.X, cell.Y))
                {
                    this.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        public bool SetCharacterPosition(Character character, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                this.PickUpTreasure(character, x, y);
                this.SetIsWalkable(character.X, character.Y, true);

                character.X = x;
                character.Y = y;

                this.SetIsWalkable(character.X, character.Y, false);

                this.OpenDoor(character, x, y);

                if (character is Player)
                {
                    this.UpdatePlayerFieldOfView();
                }

                return true;
            }

            return false;
        }

        public void AddPlayer(Player player)
        {
            Engine.Player = player;
            this.SetIsWalkable(player.X, player.Y, false);
            this.UpdatePlayerFieldOfView();
        }

        public void AddGold(int x, int y, int amount)
        {
            if (amount > 0)
            {
                this.AddTreasure(x, y, new Gold(amount));
            }
        }

        public void AddTreasure(int x, int y, ITreasure treasure)
        {
            this.treasurePiles.Add(new TreasurePile(x, y, treasure));
        }

        public void AddMonster(Monster monster)
        {
            this.monsters.Add(monster);
            this.SetIsWalkable(monster.X, monster.Y, false);
            Engine.SchedulingSystem.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            this.monsters.Remove(monster);
            this.SetIsWalkable(monster.X, monster.Y, true);
            Engine.SchedulingSystem.Remove(monster);
        }

        public Door GetDoor(int x, int y)
        {
            return this.Doors.SingleOrDefault(d => d.X == x && d.Y == y);
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return this.monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        public Point GetRandomWalkableLocationInRoom(Room room)
        {
            if (this.DoesRoomHaveWalkableSpace(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Engine.Random.Next(1, room.DungeonRoom.Width - 2) + room.DungeonRoom.X;
                    int y = Engine.Random.Next(1, room.DungeonRoom.Height - 2) + room.DungeonRoom.Y;
                    if (this.IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }

            return null;
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y);
            this.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        private void PickUpTreasure(Character actor, int x, int y)
        {
            List<TreasurePile> treasureAtLocation = this.treasurePiles.Where(g => g.X == x && g.Y == y).ToList();
            foreach (TreasurePile treasurePile in treasureAtLocation)
            {
                if (treasurePile.Treasure.PickUp(actor))
                {
                    this.treasurePiles.Remove(treasurePile);
                }
            }
        }

        private void OpenDoor(Character character, int x, int y)
        {
            Door door = this.GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = GetCell(x, y);
                this.SetCellProperties(x, y, true, true, cell.IsExplored);

                Engine.MessageLog.Add($"{character.Name} opened a door");
            }
        }

        private bool DoesRoomHaveWalkableSpace(Room room)
        {
            for (int x = 1; x <= room.DungeonRoom.Width - 2; x++)
            {
                for (int y = 1; y <= room.DungeonRoom.Height - 2; y++)
                {
                    if (this.IsWalkable(x + room.DungeonRoom.X, y + room.DungeonRoom.Y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            if (!cell.IsExplored)
            {
                return;
            }

            if (this.IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }
    }
}