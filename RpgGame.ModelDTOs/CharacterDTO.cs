using RLNET;
using RogueSharp;
using RpgGame.ModelDTOs.Interfaces;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs
{
    public abstract class CharacterDTO : ICharacter, IDrawable, IScheduleable
    {
        public virtual string Name { get; set; }

        public int Awareness { get; set; }

        public int Steps { get; set; }

        public int MinAttack { get; set; }
        public int MaxAttack { get; set; }

        public int MinDefence { get; set; }
        public int MaxDefence { get; set; }

        public int Gold { get; set; }

        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public int Speed { get; set; }
        public int Time => this.Speed;

        public virtual RLColor Color { get; set; }
        public virtual char Symbol { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.IsExplored(X, Y))
            {
                return;
            }

            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
