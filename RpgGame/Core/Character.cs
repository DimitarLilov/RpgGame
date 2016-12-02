namespace RpgGame.Core
{
    using Interfaces;
    using RLNET;
    using RogueSharp;

    public abstract class Character : ICharacter, IDrawable
    {
        public string Name { get; set; }

        public int Awareness { get; set; }

        public RLColor Color { get; set; }

        public char Symbol { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(this.X, this.Y).IsExplored)
            {
                return;
            }

            if (map.IsInFov(this.X, this.Y))
            {
                console.Set(this.X, this.Y, this.Color, Colors.FloorBackgroundFov, this.Symbol);
            }
            else
            {
                console.Set(this.X, this.Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
