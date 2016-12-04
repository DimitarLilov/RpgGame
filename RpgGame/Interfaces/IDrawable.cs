namespace RpgGame.Interfaces
{
    using RLNET;
    using RogueSharp;

    public interface IDrawable
    {
        RLColor Color { get; set; }

        char Symbol { get; }

        int X { get; set; }

        int Y { get; set; }

        void Draw(RLConsole console, IMap map);
    }
}
