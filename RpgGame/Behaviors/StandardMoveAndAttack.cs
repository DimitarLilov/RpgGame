
using RpgGame.Core.System;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Map;
using RpgGame.ModelDTOs.Monsters;
using RpgGame.Models;
using RpgGame.Utilities.Utilities;

namespace RpgGame.Behaviors
{
    using System.Linq;
    using RogueSharp;
    using RpgGame.Interfaces;
    using RpgGame.Utilities;

    public class StandardMoveAndAttack : IBehavior
    {
        public bool Act(MonsterDTO monster, DungeonMapDTO map, PlayerDTO player, CommandSystem commandSystem)
        {
            var monsterFov = new FieldOfView(map);

            if (!monster.TurnsAlerted.HasValue)
            {
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
                if (monsterFov.IsInFov(player.X, player.Y))
                {
                    MessageLog.Add($"{monster.Name} is eager to fight {player.Name}");
                    monster.TurnsAlerted = 1;
                }
            }

            if (monster.TurnsAlerted.HasValue)
            {
                map.SetIsWalkable(monster.X, monster.Y, true);
                map.SetIsWalkable(player.X, player.Y, true);

                var pathFinder = new PathFinder(map);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(
                    map.GetCell(monster.X, monster.Y),
                    map.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    MessageLog.Add($"{monster.Name} waits for a turn");
                }

                map.SetIsWalkable(monster.X, monster.Y, false);
                map.SetIsWalkable(player.X, player.Y, false);

                if (path != null)
                {
                    try
                    {
                        commandSystem.MoveMonster(monster, map, player, path.Steps.First());
                    }
                    catch (NoMoreStepsException)
                    {
                        MessageLog.Add($"{monster.Name} growls in frustration");
                    }
                }

                monster.TurnsAlerted++;

                if (monster.TurnsAlerted > 15)
                {
                    monster.TurnsAlerted = null;
                }
            }
            return true;
        }
    }
}