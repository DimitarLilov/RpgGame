using System.Linq;
using AutoMapper;
using RLNET;
using RpgGame.Data;
using RpgGame.Data.Data;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Map;
using RpgGame.Utilities.Utilities;

namespace RpgGame.Core.System
{
    public class GraphicsManager
    {
        private readonly MappingService mappingService;

        public GraphicsManager(MappingService mappingService)
        {
            this.mappingService = mappingService;
        }

        public void Draw(RLConsole mapConsole, RLConsole statConsole, RLConsole messageConsole)
        {
            var mapDto = this.mappingService.GetDungeonDtoById(1);
            var playerDto = this.mappingService.GetPlayerDtoById(1);
            mapDto.Initialize();
            mapDto.UpdatePlayerFieldOfView(playerDto);
            mapDto.Draw(mapConsole, statConsole);

            playerDto.Draw(mapConsole, mapDto);
            playerDto.DrawStats(statConsole);

            MessageLog.Draw(messageConsole);
        }
    }
}