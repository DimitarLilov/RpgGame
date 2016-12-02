namespace RpgGame.Core
{
    using Interfaces;
    using RLNET;
    using RogueSharp;

    public class Door : IDrawable
    {
        public Door()
        {
            this.Symbol = '+';
            this.Color = Colors.Door;
            this.BackgroundColor = Colors.DoorBackground;
        }

        public bool IsOpen { get; set; }

        public RLColor Color { get; set; }

        public RLColor BackgroundColor { get; set; }

        public char Symbol { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(this.X, this.Y).IsExplored)
            {
                return;
            }

            this.Symbol = this.IsOpen ? '-' : '+';
            if (map.IsInFov(this.X, this.Y))
            {
                this.Color = Colors.DoorFov;
                this.BackgroundColor = Colors.DoorBackgroundFov;
            }
            else
            {
                this.Color = Colors.Door;
                this.BackgroundColor = Colors.DoorBackground;
            }

            console.Set(this.X, this.Y, this.Color, this.BackgroundColor, this.Symbol);
        }
    }
}