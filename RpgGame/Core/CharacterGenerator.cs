using System;
using System.Collections.Generic;
using System.Linq;
using RogueSharp;
using RpgGame.Core.System;
using RpgGame.Data;
using RpgGame.Data.Data;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Map;
using RpgGame.ModelDTOs.Monsters;
using RpgGame.Models;

namespace RpgGame.Core
{
    public class CharacterGenerator
    {
        private readonly MappingService mappingService;
        private readonly List<Point> takenCells;

        public CharacterGenerator(MappingService mappingService)
        {
            this.takenCells = new List<Point>();
            this.mappingService = mappingService;
        }

        public void CreatePlayer(Player player, int mapId)
        {
            var map = this.mappingService.GetDungeonById(mapId);

            player.X = map.Rooms.FirstOrDefault().XCenter;
            player.Y = map.Rooms.FirstOrDefault().YCenter;

            this.mappingService.AddPlayer(player);
        }

        public void CreateMonsters(int mapId)
        {
            var rnd = new Random();
            var dungeon = this.mappingService.GetDungeonById(mapId);

            foreach (var room in dungeon.Rooms)
            {
                if (rnd.Next(1, 10) >= 7)
                {
                    continue;
                }

                var numberOfMonsters = rnd.Next(1, 5);
                for (int i = 0; i < numberOfMonsters; i++)
                {
                    var randomRoomLocation = this.GetRandomWalkableLocationInRoom(room);
                    if (randomRoomLocation == null)
                    {
                        continue;
                    }

                    var monster = new Orc
                    {
                        Name = nameof(Orc),
                        Awareness = 10,
                        MinAttack = 5,
                        MaxAttack = 25,

                        MinDefence = 0,
                        MaxDefence = 15,

                        Gold = 10,
                        Health = 20,
                        MaxHealth = 20,

                        Speed = 14,

                        X = randomRoomLocation.X,
                        Y = randomRoomLocation.Y,

                        DungeonMap = dungeon
                    };

                   this.mappingService.AddMonster(monster);
                }
            }
        }

        public void GeneratePlayer(int mapId, int playerId, SchedulingSystem schedulingSystem)
        {
            var mapDto = this.mappingService.GetDungeonDtoById(mapId);
            var playerDto = this.mappingService.GetPlayerDtoById(playerId);

            mapDto.SetIsWalkable(playerDto.X, playerDto.Y, false);
            mapDto.UpdatePlayerFieldOfView(playerDto);

            schedulingSystem.Add(playerDto);
        }

        public void GenerateMonsters(int mapId, SchedulingSystem schedulingSystem)
        {
            var dungeonDto = this.mappingService.GetDungeonDtoById(mapId);
            var monsterDtos = dungeonDto.Monsters;
            foreach (var monsterDto in monsterDtos)
            {
                dungeonDto.SetIsWalkable(monsterDto.X, monsterDto.Y, false);

                schedulingSystem.Add(monsterDto);
            }
        }

        private Point GetRandomWalkableLocationInRoom(Room room)
        {
            var rnd = new Random();

            while (true)
            {
                int x = rnd.Next(1, room.Width - 2) + room.X;
                int y = rnd.Next(1, room.Height - 2) + room.Y;

                if (this.takenCells.Any(c => c.X == x && c.Y == y))
                {
                    continue;
                }

                var point = new Point(x, y);
                this.takenCells.Add(point);

                return point;
            }
        } }
}