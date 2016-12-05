namespace RpgGame.Core
{
    using System;
    using System.Text;
    using RogueSharp;
    using RpgGame.Enums;
    using RpgGame.Interfaces;
    using RpgGame.Models;
    using RpgGame.Models.Map;
    using RpgGame.Models.Monsters;
    using RpgGame.Utilities;

    public class CommandSystem
    {
        private readonly TempDatabase db;
        private readonly SchedulingSystem schedulingSystem;

        private readonly StringBuilder attackReport;

        public CommandSystem(TempDatabase db, SchedulingSystem schedulingSystem)
        {
            this.db = db;
            this.schedulingSystem = schedulingSystem;
            this.attackReport = new StringBuilder();
        }

        public TempDatabase Database => this.db;

        public bool IsPlayerTurn { get; set; }

        public bool MovePlayer(Direction direction)
        {
            var player = this.db.Player;
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

            var monster = this.db.DungeonMap.GetMonsterAt(x, y);

            if (monster != null)
            {
                this.Attack(player, monster, this.db.DungeonMap);
                return true;
            }

            return this.db.DungeonMap.SetCharacterPosition(player, x, y);
        }

        public void ActivateMonsters()
        {
            IScheduleable scheduleable = this.schedulingSystem.Get();
            if (scheduleable is Player)
            {
                this.IsPlayerTurn = true;
                this.schedulingSystem.Add(this.db.Player);
            }
            else
            {
                var monster = scheduleable as Monster;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    this.schedulingSystem.Add(monster);
                }

                this.ActivateMonsters();
            }
        }

        public void MoveMonster(Monster monster, Cell cell)
        {
            if (!this.db.DungeonMap.SetCharacterPosition(monster, cell.X, cell.Y))
            {
                if (this.db.Player.X == cell.X && this.db.Player.Y == cell.Y)
                {
                    this.Attack(monster, this.db.Player, this.db.DungeonMap);
                }
            }
        }

        public void CreateMainModels(Player player)
        {
            this.db.Player = player;

            MapGenerator mapGenerator =
             new MapGenerator(Constants.MapWidth, Constants.MapHeight);

            this.db.DungeonMap = mapGenerator.CreateMap(player, this.schedulingSystem);
        }

        public void EndPlayerTurn()
        {
            this.IsPlayerTurn = false;
        }

        private void Attack(Character attacker, Character defender, DungeonMap map)
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


        private void ResolveDeath(Character defender, DungeonMap map)
        {
            if (defender is Player)
            {
                this.attackReport.Append($"Player {defender.Name} was killed!");
            }
            else if (defender is Monster)
            {
                map.RemoveMonster((Monster)defender, this.schedulingSystem);
                map.AddGold(defender.X, defender.Y, defender.Gold);
                this.attackReport.Append($"{defender.Name} died and dropped {defender.Gold} gold.");
            }
        }
    }
}
