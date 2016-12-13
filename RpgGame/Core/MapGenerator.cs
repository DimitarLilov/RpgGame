using System;
using System.Collections.Generic;
using System.Linq;
using RogueSharp;
using RpgGame.Data;
using RpgGame.Data.Data;
using RpgGame.ModelDTOs.Map;
using RpgGame.Models;
using RpgGame.Utilities;

namespace RpgGame.Core
{
    using System;

    public class MapGenerator
    {
        private DungeonMap map;

        private readonly MappingService mappingService;

        public MapGenerator(MappingService mappingService)
        {
            this.mappingService = mappingService;
        }

        public void GenerateMap(int dungeonId)
        {
            var dungeonDto = this.mappingService.GetDungeonDtoById(dungeonId);
            dungeonDto.Generate();
        }

        public void CreateMap(int width, int height, int level)
        {
            this.map = new DungeonMap()
            {
                Width = width,
                Height = height,
                Level = level
            };

            this.mappingService.AddDungeon(this.map);

            var rooms = new Room[]
            {
                new Room
                {
                    X = 5,
                    Y = 5,
                    Height = 10,
                    Width = 15
                },
                  new Room
                {
                    X = 50,
                    Y = 5,
                    Height = 10,
                    Width = 15
                },
                    new Room
                {
                    X = 30,
                    Y = 30,
                    Height = 10,
                    Width = 15
                }
            };

            this.mappingService.AddRoomsToDungeon(rooms, this.map.Id);

            for (int r = 1; r < rooms.Count(); r++)
            {
                var previuosRoom = rooms[r - 1];
                var currentRoom = rooms[r];

                int previousRoomCenterX = previuosRoom.XCenter + previuosRoom.Width / 2;
                int previousRoomCenterY = previuosRoom.YCenter;
                int currentRoomCenterX = currentRoom.XCenter - currentRoom.Width / 2;
                int currentRoomCenterY = currentRoom.YCenter;

                this.CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY, true);
                this.CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX, false);

                this.CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX, true);
                this.CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY, false);
            }
        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition, bool entranceDoor)
        {
            var tunel = new Tunel()
            {
                XStart = xStart,
                XEnd = xEnd,
                YStart = yPosition,
                YEnd = yPosition
            };

            Door door;

            if (entranceDoor)
            {
                door = new Door()
                {
                    X = xStart,
                    Y = yPosition
                };
            }
            else
            {
                door = new Door()
                {
                    X = xEnd,
                    Y = yPosition
                };
            }

            tunel.Door = door;

            this.mappingService.AddTunelToDungeon(tunel, this.map.Id);
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition, bool entranceDoor)
        {

            var tunel = new Tunel()
            {
                XStart = xPosition,
                XEnd = xPosition,
                YStart = yStart,
                YEnd = yEnd
            };

            Door door;

            if (entranceDoor)
            {
                door = new Door()
                {
                    X = xPosition,
                    Y = yStart
                };
            }
            else
            {
                door = new Door()
                {
                    X = xPosition,
                    Y = yEnd
                };
            }

            tunel.Door = door;

            this.mappingService.AddTunelToDungeon(tunel, this.map.Id);
        }
    }
}