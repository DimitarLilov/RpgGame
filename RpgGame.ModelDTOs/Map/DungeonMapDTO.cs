using System;
using System.Collections.Generic;
using System.Linq;
using RLNET;
using RpgGame.ModelDTOs.Interfaces;
using RpgGame.ModelDTOs.Monsters;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs.Map
{
    using RogueSharp;

    public class DungeonMapDTO : Map
    {
        private readonly List<TreasurePile> treasurePiles;

        public DungeonMapDTO(int level)
        {
            this.Monsters = new List<MonsterDTO>();
            this.Rooms = new List<RoomDTO>();
            this.treasurePiles = new List<TreasurePile>();
            this.Tunels = new List<TunelDTO>();

            this.Level = level;
        }

        public int Level { get; set; }

        public List<RoomDTO> Rooms { get; set; }

        public List<TunelDTO> Tunels { get; set; }

        public List<MonsterDTO> Monsters { get; set; }

        public void Initialize()
        {
            this.Initialize(this.Width, this.Height);
            this.Generate();
        }

        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in this.GetAllCells())
            {
                this.SetConsoleSymbolForCell(mapConsole, cell);
            }

            int i = 0;

            foreach (MonsterDTO monster in this.Monsters)
            {
                var orc = (OrcDTO)monster;
                orc.Draw(mapConsole, this);

                if (this.IsInFov(orc.X, orc.Y))
                {
                    orc.DrawStats(statConsole, i);
                    i++;
                }
            }

            foreach (TreasurePile treasurePile in this.treasurePiles)
            {
                IDrawable drawableTreasure = treasurePile.Treasure as IDrawable;
                drawableTreasure?.Draw(mapConsole, this);
            }

            foreach (TunelDTO tunel in this.Tunels)
            {
                tunel.Door.Draw(mapConsole, this);
            }
        }

        public void UpdatePlayerFieldOfView(PlayerDTO player)
        {
            this.ComputeFov(player.X, player.Y, player.Awareness, true);
            foreach (Cell cell in GetAllCells())
            {
                if (this.IsInFov(cell.X, cell.Y))
                {
                    this.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

<<<<<<< HEAD:RpgGame.ModelDTOs/Map/DungeonMapDTO.cs
        public bool SetCharacterPosition(CharacterDTO character, int x, int y)
=======
        public bool SetCharacterPosition(CharacterDto character, int x, int y)
>>>>>>> origin/master:RpgGame/Models/Map/DungeonMap.cs
        {
            if (this.GetCell(x, y).IsWalkable)
            {
                this.PickUpTreasure(character, x, y);
                this.SetIsWalkable(character.X, character.Y, true);
                this.SetIsWalkable(x, y, false);

                this.OpenDoor(character, x, y);

                if (character is PlayerDTO)
                {
                    this.UpdatePlayerFieldOfView((PlayerDTO)character);
                }

                return true;
            }

            return false;
        }

        public void AddGold(int x, int y, int amount)
        {
            if (amount > 0)
            {
                this.AddTreasure(x, y, new GoldDTO(amount));
            }
        }

        public void AddTreasure(int x, int y, ITreasure treasure)
        {
            this.treasurePiles.Add(new TreasurePile(x, y, treasure));
        }

        public DoorDTO GetDoor(int x, int y)
        {
            return this.Tunels.Select(t => t.Door).FirstOrDefault(d => d.X == x && d.Y == y);
        }

        public MonsterDTO GetMonsterAt(int x, int y)
        {
            return this.Monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = this.GetCell(x, y);
            this.SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

<<<<<<< HEAD:RpgGame.ModelDTOs/Map/DungeonMapDTO.cs
        private void PickUpTreasure(CharacterDTO actor, int x, int y)
=======
        private void PickUpTreasure(CharacterDto actor, int x, int y)
>>>>>>> origin/master:RpgGame/Models/Map/DungeonMap.cs
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

<<<<<<< HEAD:RpgGame.ModelDTOs/Map/DungeonMapDTO.cs
        private void OpenDoor(CharacterDTO character, int x, int y)
=======
        private void OpenDoor(CharacterDto character, int x, int y)
>>>>>>> origin/master:RpgGame/Models/Map/DungeonMap.cs
        {
            DoorDTO door = this.GetDoor(x, y);
            if (door != null && !door.IsOpen)
            {
                door.IsOpen = true;
                var cell = this.GetCell(x, y);
                this.SetCellProperties(x, y, true, true, cell.IsExplored);

                MessageLog.Add($"{character.Name} opened a door");
            }
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

        public void Generate()
        {
            foreach (RoomDTO room in this.Rooms)
            {
                this.SetRooms(room);
            }

            foreach (TunelDTO tunel in this.Tunels)
            {
                if (tunel.XStart == tunel.XEnd)
                {
                    this.SetVerticalTunnel(tunel.YStart, tunel.YEnd, tunel.XStart);
                }
                else
                {
                    this.SetHorizontalTunnel(tunel.XStart, tunel.XEnd, tunel.YStart);
                }

                this.SetDoors(tunel);
            }
        }

        private void SetHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                this.SetCellProperties(x, yPosition, true, true);
            }
        }

        private void SetVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                this.SetCellProperties(xPosition, y, true, true);
            }
        }

        private void SetDoors(TunelDTO tunel)
        {
            this.SetCellProperties(tunel.Door.X, tunel.Door.Y, false, true, false);
        }

        private void SetRooms(RoomDTO room)
        {
            for (int x = room.DungeonRoom.Left + 1; x < room.DungeonRoom.Right; x++)
            {
                for (int y = room.DungeonRoom.Top + 1; y < room.DungeonRoom.Bottom; y++)
                {
                    this.SetCellProperties(x, y, true, true);
                }
            }
        }
    }
}