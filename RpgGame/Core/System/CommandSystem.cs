namespace RpgGame.Core
{
    using System.Text;
    using RogueSharp;
    using RogueSharp.DiceNotation;
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

        public CommandSystem(TempDatabase db, SchedulingSystem schedulingSystem)
        {
            this.db = db;
            this.schedulingSystem = schedulingSystem;
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
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = this.ResolveAttack(attacker, defender, attackMessage);

            int blocks = this.ResolveDefense(defender, hits, attackMessage, defenseMessage);

            MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            this.ResolveDamage(defender, damage, map);
        }

        private int ResolveAttack(Character attacker, Character defender, StringBuilder attackMessage)
        {
            int hits = 0;

            attackMessage.AppendFormat("{0} attacks {1} and rolls: ", attacker.Name, defender.Name);

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }

            return hits;
        }

        private int ResolveDefense(Character defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("  {0} defends and rolls: ", defender.Name);

                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();


                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        private void ResolveDamage(Character defender, int damage, DungeonMap map)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                MessageLog.Add($"  {defender.Name} was hit for {damage} damage");

                if (defender.Health <= 0)
                {
                    this.ResolveDeath(defender, map);
                }
            }
            else
            {
                MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        private void ResolveDeath(Character defender, DungeonMap map)
        {
            if (defender is Player)
            {
                MessageLog.Add($"  {defender.Name} was killed, GAME OVER MAN!");
            }
            else if (defender is Monster)
            {
                map.RemoveMonster((Monster)defender, this.schedulingSystem);

                MessageLog.Add($"  {defender.Name} died and dropped {defender.Gold} gold");
            }
        }
    }
}
