namespace RpgGame.Models.Monsters
{
    using System;
    using RLNET;
    using RpgGame.Core;
    using RpgGame.Interfaces;
    using RpgGame.Utilities;

    public abstract class Monster : CharacterDto
    {
        protected Monster(int awareness, int minAttack, int maxAttack,
            int minDefence, int maxDefence, int gold, int health, int maxHealth,
            int speed, RLColor color, char symbol, int x, int y)
            : base(null, awareness, minAttack, maxAttack, minDefence, maxDefence,
                  gold, health, maxHealth, speed, color, symbol, x, y)
        {
        }

        public override string Name => this.GetType().Name;
        public int? TurnsAlerted { get; set; }

        public IBehavior Behavior { get; set; }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            this.Behavior.Act(this, commandSystem);
        }

        public void DrawStats(RLConsole statConsole, int position)
        {
            int yPosition = 13 + (position * 2);

            statConsole.Print(1, yPosition, this.Symbol.ToString(), this.Color);

            int width = Convert.ToInt32((this.Health / (double)this.MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            statConsole.SetBackColor(3, yPosition, width, 1, Swatch.DbBlood);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Swatch.PrimaryDarkest);

            statConsole.Print(2, yPosition, $": {this.GetType().Name}", Swatch.DbLight);
        }
    }
}
