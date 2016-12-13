using System;
using AutoMapper;
using RpgGame.Core.System;
using RpgGame.Data;
using RpgGame.Data.Interfaces;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Map;
using RpgGame.Models;
using RpgGame.Utilities.Utilities;

namespace RpgGame.Core
{
    public class ObjectManager
    {
        private readonly MapGenerator mapGenerator;
        private readonly CharacterGenerator charGenerator;

        private readonly SchedulingSystem schedulingSystem;

        public ObjectManager(SchedulingSystem schedulingSystem, MappingService mappingService)
        {
            this.mapGenerator = new MapGenerator(mappingService);
            this.charGenerator = new CharacterGenerator(mappingService);

            this.schedulingSystem = schedulingSystem;
        }

        public void CreateMainModels(Player player)
        {
            this.mapGenerator.CreateMap(Constants.MapWidth, Constants.MapHeight, 1);
            this.charGenerator.CreatePlayer(player, 1);
            this.charGenerator.CreateMonsters(1);
        }

        public void GenerateObjects(int level)
        {
            this.mapGenerator.GenerateMap(1);
            this.charGenerator.GeneratePlayer(1, 1, this.schedulingSystem);
            this.charGenerator.GenerateMonsters(1, this.schedulingSystem);
        }
    }
}