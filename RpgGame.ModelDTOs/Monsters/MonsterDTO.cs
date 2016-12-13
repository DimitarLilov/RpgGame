using System;
using RLNET;
using RpgGame.ModelDTOs.Map;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs.Monsters
{
    public class MonsterDTO : CharacterDTO
    {
        public override string Name => this.GetType().Name.Replace("DTO", "");

        public int? TurnsAlerted { get; set; }

        public void DrawStats(RLConsole statConsole, int position)
        {
            int yPosition = 13 + (position * 2);

            statConsole.Print(1, yPosition, this.Symbol.ToString(), this.Color);

            int width = Convert.ToInt32((this.Health / (double)this.MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            statConsole.SetBackColor(3, yPosition, width, 1, Swatch.DbBlood);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Swatch.PrimaryDarkest);

            statConsole.Print(2, yPosition, $": {this.Name}", Swatch.DbLight);
        }
    }
}
