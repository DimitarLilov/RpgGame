namespace RpgGame.Core
{
    public class Player : Character
    {
        public Player()
        {
            this.Awareness = 15;
            this.Name = "Duci";
            this.Color = Colors.Player;
            this.Symbol = '@';
            this.X = 10;
            this.Y = 10;
        }
    }
}
