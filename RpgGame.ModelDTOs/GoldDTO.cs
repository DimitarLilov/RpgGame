using RLNET;
using RogueSharp;
using RpgGame.ModelDTOs.Interfaces;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs
{
    public class GoldDTO : ITreasure, IDrawable
    {
        public GoldDTO(int amount)
        {
            this.Amount = amount;
        }

        public int Amount { get; set; }

        public RLColor Color => RLColor.Yellow;

        public char Symbol => '$';

        public int X { get; set; }

        public int Y { get; set; }

        public bool PickUp(ICharacter character)
        {
            character.Gold += this.Amount;
            MessageLog.Add($"{character.Name} picked up {this.Amount} gold");

            return true;
        }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(this.X, this.Y))
            {
                return;
            }

            if (map.IsInFov(this.X, this.Y))
            {
                console.Set(this.X, this.Y, this.Color, Colors.FloorBackgroundFov, this.Symbol);
            }
            else
            {
                console.Set(this.X, this.Y, this.Color, Colors.FloorBackground, this.Symbol);
            }
        }
    }
}
