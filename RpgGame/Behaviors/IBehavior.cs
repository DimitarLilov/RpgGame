
using RpgGame.Core.System;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Map;
using RpgGame.ModelDTOs.Monsters;

namespace RpgGame.Interfaces
{

    public interface IBehavior
    {
        bool Act(MonsterDTO monster, DungeonMapDTO map, PlayerDTO player, CommandSystem commandSystem);
    }
}
