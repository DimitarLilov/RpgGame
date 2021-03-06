﻿namespace RpgGame.Models
{
    using RLNET;
    using RpgGame.Utilities;

    public class Player : CharacterDto
    {
        private const int DefaultAwareness = 15;

        private const int DefaultMinAttack = 15;
        private const int DefaultMaxAttack = 40;

        private const int DefaultMinDefence = 10;
        private const int DefaultMaxDefence = 30;

        private const int DefaultGold = 0;
        private const int DefaultHealth = 100;
        private const int DefaultMaxHealth = 100;

        private const int DefaultSpeed = 10;
        private const char DefaultSymbol = '@';

        public Player(string name)
            : base(name, DefaultAwareness,
                  DefaultMinAttack, DefaultMaxAttack,
                  DefaultMinDefence, DefaultMaxDefence,
                  DefaultGold, DefaultHealth, DefaultMaxHealth,
                  DefaultSpeed, Colors.Player, DefaultSymbol, 10, 10)
        {
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {this.Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {this.Health}/{this.MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {this.MinAttack}-{this.MaxAttack}", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {this.MinDefence}-{this.MaxDefence}", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {this.Gold}", Colors.Gold);
        }
    }
}
