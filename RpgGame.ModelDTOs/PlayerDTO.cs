using RLNET;
using RpgGame.ModelDTOs.Interfaces;
using RpgGame.Utilities.Utilities;

namespace RpgGame.ModelDTOs
{
    public class PlayerDTO : CharacterDTO, IScheduleable
    {
        public override RLColor Color => Colors.Player;

        public override char Symbol => '@';

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {this.Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {this.Health}/{this.MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {this.MinAttack}-{this.MaxAttack}", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {this.MinDefence}-{this.MaxDefence}", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {this.Gold}", Colors.Gold);
        }
    }
}
