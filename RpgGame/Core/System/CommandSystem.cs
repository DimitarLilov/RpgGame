using System;
using System.Linq;
using System.Text;
using AutoMapper;
using RogueSharp;
using RpgGame.Behaviors;
using RpgGame.Data;
using RpgGame.Data.Data;
using RpgGame.Enums;
using RpgGame.Interfaces;
using RpgGame.ModelDTOs;
using RpgGame.ModelDTOs.Interfaces;
using RpgGame.ModelDTOs.Map;
using RpgGame.ModelDTOs.Monsters;
using RpgGame.Utilities.Utilities;

namespace RpgGame.Core.System
{
    public class CommandSystem
    {
        private readonly SchedulingSystem schedulingSystem;
        private readonly MappingService mappingService;

        private readonly StringBuilder attackReport;

        public CommandSystem(SchedulingSystem schedulingSystem, MappingService mappingService)
        {
            this.mappingService = mappingService;
            this.schedulingSystem = schedulingSystem;

            this.attackReport = new StringBuilder();
        }

        public bool IsPlayerTurn { get; set; }

        public bool MovePlayer(Direction direction, int id, int mapId)
        {
            var player = this.mappingService.GetPlayerById(id);
            var playerDto = this.mappingService.GetPlayerDtoById(1);

            int x = player.X;
            int y = player.Y;

            switch (direction)
            {
                case Direction.Up:
                    y = player.Y - 1;
                    break;
                case Direction.Down:
                    y = player.Y + 1;
                    break;
                case Direction.Left:
                    x = player.X - 1;
                    break;
                case Direction.Right:
                    x = player.X + 1;
                    break;
                default:
                    return false;
            }

            var map = this.mappingService.GetDungeonDtoById(id);

            var monster = map.GetMonsterAt(x, y);

            if (monster != null)
            {
                this.Attack(playerDto, monster, map);
                return true;
            }

            bool canMove = map.SetCharacterPosition(playerDto, x, y);

            if (!canMove)
            {
                return false;
            }

            player.X = x;
            player.Y = y;
            this.mappingService.UnitOfWork.Commit();
            return true;
        }

        public void ActivateMonsters(int mapId, int playerId)
        {
            var player = this.mappingService.GetPlayerDtoById(playerId);
            var map = this.mappingService.GetDungeonDtoById(mapId);
            IScheduleable scheduleable = this.schedulingSystem.Get();
            if (scheduleable is PlayerDTO)
            {
                this.IsPlayerTurn = true;
                this.schedulingSystem.Add(scheduleable);
            }
            else
            {
                var monsterDto = scheduleable as MonsterDTO;

                if (monsterDto != null)
                {
                    var behavior = new StandardMoveAndAttack();
                    behavior.Act(monsterDto, map, player, this);
                    this.schedulingSystem.Add(monsterDto);
                }

                this.ActivateMonsters(1, 1);
            }
        }

        public void MoveMonster(MonsterDTO monsterDto, DungeonMapDTO map, PlayerDTO player, Cell cell)
        {
            if (!map.SetCharacterPosition(monsterDto, cell.X, cell.Y))
            {
                if (player.X == cell.X && player.Y == cell.Y)
                {
                    this.Attack(monsterDto, player, map);
                }
            }
            else
            {
                var monster = this.mappingService.GetMonsterByPosition(monsterDto.X, monsterDto.Y);
                monster.X = cell.X;
                monster.Y = cell.Y;
                monsterDto.X = cell.X;
                monsterDto.Y = cell.Y;
                this.mappingService.UnitOfWork.Commit();
            }
        }

        public void EndPlayerTurn()
        {
            this.IsPlayerTurn = false;
        }

        private void Attack(CharacterDTO attacker, CharacterDTO defender, DungeonMapDTO map)
        {
            var rnd = new Random();

            int attack = rnd.Next(attacker.MinAttack, attacker.MaxAttack);
            int block = rnd.Next(defender.MinDefence, defender.MaxDefence);
            int damage = attack - block;

            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                this.attackReport.Append($"{defender.Name} was hit with {damage} damage by {attacker.Name}. ");

                if (defender.Health <= 0)
                {
                    this.ResolveDeath(defender, map);
                }
            }
            else
            {
                this.attackReport.Append($"{defender.Name} blocked all damage from {attacker.Name}. ");
            }

            if (string.IsNullOrWhiteSpace(this.attackReport.ToString()))
            {
                return;
            }

            MessageLog.Add(this.attackReport.ToString());
            this.attackReport.Clear();
        }

        private void ResolveDeath(CharacterDTO defender, DungeonMapDTO map)
        {
            if (defender is PlayerDTO)
            {
                this.attackReport.Append($"Player {defender.Name} was killed!");
            }
            else if (defender is MonsterDTO)
            {
                this.RemoveMonster((MonsterDTO)defender, map);
                map.AddGold(defender.X, defender.Y, defender.Gold);
                this.attackReport.Append($"{defender.Name} died and dropped {defender.Gold} gold.");
            }
        }

        public void RemoveMonster(MonsterDTO monster, DungeonMapDTO map)
        {
            map.SetIsWalkable(monster.X, monster.Y, true);
            this.schedulingSystem.Remove(monster);
        }
    }
}
