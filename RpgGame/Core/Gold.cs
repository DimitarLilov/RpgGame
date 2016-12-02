namespace RpgGame.Core
{
    using RLNET;
    using RogueSharp;
    using RpgGame.Interfaces;

    public class Gold : ITreasure, IDrawable
    {
        public Gold(int amount)
        {
            this.Amount = amount;
            this.Symbol = '$';
            this.Color = RLColor.Yellow;
        }

        public int Amount { get; set; }

        public RLColor Color { get; set; }

        public char Symbol { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public bool PickUp(ICharacter character)
        {
            character.Gold += this.Amount;
            Engine.MessageLog.Add($"{character.Name} picked up {Amount} gold");
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
