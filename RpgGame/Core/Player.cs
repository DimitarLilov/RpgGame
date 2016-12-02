namespace RpgGame.Core
{
    using RLNET;

    public class Player : Character
    {
        public Player()
        {
            this.Attack = 2;
            this.AttackChance = 50;
            this.Awareness = 15;
            this.Name = "Duci";
            this.Color = Colors.Player;
            this.Symbol = '@';
            this.Defense = 2;
            this.DefenseChance = 40;
            this.Gold = 0;
            this.Health = 100;
            this.MaxHealth = 100;
            this.Speed = 10;
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {Attack} ({AttackChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense} ({DefenseChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {Gold}", Colors.Gold);
        }
    }
}
