namespace RpgGame.Core
{
    using System;
    using Systems;
    using Behaviors;
    using RLNET;

    public class Monster : Character
    {
        public int? TurnsAlerted { get; set; }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }

        public void DrawStats(RLConsole statConsole, int position)
        {
            int yPosition = 13 + (position * 2);

            statConsole.Print(1, yPosition, this.Symbol.ToString(), this.Color);

            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0f);
            int remainingWidth = 16 - width;

            statConsole.SetBackColor(3, yPosition, width, 1, Swatch.Red);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Swatch.DarkRed);

            statConsole.Print(2, yPosition, $": {Name}", Swatch.DbLight);
        }
    }
}
