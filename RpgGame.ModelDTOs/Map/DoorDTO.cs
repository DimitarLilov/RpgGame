using RLNET;
using RogueSharp;
using RpgGame.ModelDTOs.Interfaces;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs.Map
{
    public class DoorDTO : IDrawable
    {
        public bool IsOpen { get; set; }

        public RLColor Color => Colors.Door;

        public RLColor BackgroundColor => Colors.DoorBackground;

        public char Symbol => this.IsOpen ? '-' : '+';

        public int X { get; set; }

        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(this.X, this.Y).IsExplored)
            {
                return;
            }

       //  if (map.IsInFov(this.X, this.Y))
       //  {
       //      this.Color = Colors.DoorFov;
       //      this.BackgroundColor = Colors.DoorBackgroundFov;
       //  }
       //  else
       //  {
       //      this.Color = Colors.Door;
       //      this.BackgroundColor = Colors.DoorBackground;
       //  }

            console.Set(this.X, this.Y, this.Color, this.BackgroundColor, this.Symbol);
        }
    }
}