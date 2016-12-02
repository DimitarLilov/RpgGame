namespace RpgGame.Behaviors
{
    using System.Linq;
    using RogueSharp;
    using RpgGame.Core;
    using RpgGame.Interfaces;
    using RpgGame.Systems;

    public class StandardMoveAndAttack : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap dungeonMap = Engine.DungeonMap;
            Player player = Engine.Player;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            if (!monster.TurnsAlerted.HasValue)
            {
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
                if (monsterFov.IsInFov(player.X, player.Y))
                {
                    Engine.MessageLog.Add($"{monster.Name} is eager to fight {player.Name}");
                    monster.TurnsAlerted = 1;
                }
            }

            if (monster.TurnsAlerted.HasValue)
            {
                dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
                dungeonMap.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(dungeonMap);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(
                        dungeonMap.GetCell(monster.X, monster.Y),
                        dungeonMap.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    Engine.MessageLog.Add($"{monster.Name} waits for a turn");
                }

                dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
                dungeonMap.SetIsWalkable(player.X, player.Y, false);

                if (path != null)
                {
                    try
                    {
                        commandSystem.MoveMonster(monster, path.Steps.First());
                    }
                    catch (NoMoreStepsException)
                    {
                        Engine.MessageLog.Add($"{monster.Name} growls in frustration");
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