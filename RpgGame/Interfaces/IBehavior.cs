namespace RpgGame.Interfaces
{
    using RpgGame.Core;
    using RpgGame.Models.Monsters;

    public interface IBehavior
    {
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}
