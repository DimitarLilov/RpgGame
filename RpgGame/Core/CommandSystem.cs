namespace RpgGame.Core
{
    using System.Text;
    using RogueSharp;
    using RogueSharp.DiceNotation;
    using RpgGame.Enums;
    using RpgGame.Interfaces;
    using RpgGame.Models;
    using RpgGame.Models.Monsters;
    using RpgGame.Utilities;

    public class CommandSystem
    {
        public bool IsPlayerTurn { get; set; }

        public void ActivateMonsters()
        {
            IScheduleable scheduleable = Engine.SchedulingSystem.Get();
            if (scheduleable is Player)
            {
                this.IsPlayerTurn = true;
                Engine.SchedulingSystem.Add(Engine.Player);
            }
            else
            {
                Monster monster = scheduleable as Monster;

                if (monster != null)
                {
                    monster.PerformAction(this);
                    Engine.SchedulingSystem.Add(monster);
                }

                this.ActivateMonsters();
            }
        }

        public void EndPlayerTurn()
        {
            this.IsPlayerTurn = false;
        }

        public void MoveMonster(Monster monster, Cell cell)
        {
            if (!Engine.DungeonMap.SetCharacterPosition(monster, cell.X, cell.Y))
            {
                if (Engine.Player.X == cell.X && Engine.Player.Y == cell.Y)
                {
                    this.Attack(monster, Engine.Player);
                }
            }
        }

        public bool MovePlayer(Direction direction)
        {
            int x = Engine.Player.X;
            int y = Engine.Player.Y;

            switch (direction)
            {
                case Direction.Up:
                    {
                        y = Engine.Player.Y - 1;
                        break;
                    }

                case Direction.Down:
                    {
                        y = Engine.Player.Y + 1;
                        break;
                    }

                case Direction.Left:
                    {
                        x = Engine.Player.X - 1;
                        break;
                    }

                case Direction.Right:
                    {
                        x = Engine.Player.X + 1;
                        break;
                    }

                default:
                    {
                        return false;
                    }
            }

            if (Engine.DungeonMap.SetCharacterPosition(Engine.Player, x, y))
            {
                return true;
            }

            Monster monster = Engine.DungeonMap.GetMonsterAt(x, y);
            if (monster != null)
            {
                this.Attack(Engine.Player, monster);
                return true;
            }

            return false;
        }

        private static void ResolveDamage(Character defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                MessageLog.Add($" {defender.Name} was hit for {damage} damage");

                if (defender.Health <= 0)
                {
                    ResoveDeath(defender);
                }
            }
        }

        private static void ResoveDeath(Character defender)
        {
            if (defender is Player)
            {
                MessageLog.Add($" {defender.Name} was killed, GAME OVER!!!");
                
            }
            else if (defender is Monster)
            {
                Engine.DungeonMap.AddGold(defender.X, defender.Y, defender.Gold);
                Engine.DungeonMap.RemoveMonster((Monster)defender);

                MessageLog.Add($" {defender.Name} died and dropped {defender.Gold} gold");
            }
        }

        private static int ResolveDefense(Character defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                attackMessage.AppendFormat($"scoring {hits} hits.");
                defenseMessage.AppendFormat($"  {defender.Name} defends and rolls: ");

                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseResult = defenseDice.Roll();

                foreach (TermResult termResult in defenseResult.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    if (termResult.Value >= 100 - defender.DefenseChance)
                    {
                        blocks++;
                    }
                }

                defenseMessage.AppendFormat($"resulting {blocks} blocks.");
            }
            else
            {
                attackMessage.Append("and misses completely");
            }

            return blocks;
        }

        private static int ResolveAttack(Character attacker, Character defender, StringBuilder attackMessage)
        {
            int hits = 0;
            attackMessage.AppendFormat($"{attacker.Name} attacks {defender.Name} and rolls: ");

            DiceExpression attacDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attacResult = attacDice.Roll();

            foreach (TermResult termResult in attacResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");

                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }

            return hits;
        }

        private void Attack(Character attacker, Character defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);

            int blocks = ResolveDefense(defender, hits, attackMessage, defenseMessage);

            MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                MessageLog.Add(defenseMessage.ToString());
            }

            int damage = hits - blocks;

            ResolveDamage(defender, damage);
        }
    }
}
