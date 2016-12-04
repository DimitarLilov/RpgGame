namespace RpgGame.Models
{
    using RLNET;
    using RpgGame.Utilities;

    public class Player : Character
    {
        private const int DefaultAwareness = 15;

        private const int DefaultAttack = 2;
        private const int DefaultAttackChance = 50;

        private const int DefaultDefence = 2;
        private const int DefaultDefenceChance = 40;

        private const int DefaultGold = 0;
        private const int DefaultHealth = 100;
        private const int DefaultMaxHealth = 100;

        private const int DefaultSpeed = 10;
        private const char DefaultSymbol = '@';

        public Player(string name)
            : base(name, DefaultAwareness,
                  DefaultAttack, DefaultAttackChance,
                  DefaultDefence, DefaultDefenceChance,
                  DefaultGold, DefaultHealth, DefaultMaxHealth,
                  DefaultSpeed, Colors.Player, DefaultSymbol, 10, 10)
        {
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {this.Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {this.Health}/{this.MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {this.Attack} ({this.AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {this.Defense} ({this.DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {this.Gold}", Colors.Gold);
        }
    }
}
